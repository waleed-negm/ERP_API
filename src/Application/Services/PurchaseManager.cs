using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Extenstions;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Services
{
	public class PurchaseManager : IPurchaseManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly ISupplierJournalsManager _journalManager;
		private readonly ISupplierTransactionManager _supplierTransactionManager;
		private readonly ISupplierBalanceManager _supplierBalanceManager;

		public PurchaseManager(IApplicationDbContext db, IMapper mapper, ISupplierTransactionManager supplierTransactionManager,
			ISupplierJournalsManager journalManager, ISupplierBalanceManager supplierBalanceManager)
		{
			_db = db;
			_mapper = mapper;
			_journalManager = journalManager;
			_supplierTransactionManager = supplierTransactionManager;
			_supplierBalanceManager = supplierBalanceManager;
		}


		public PurchaseContainer NewPurchase(int SupplierId)
		{
			var vm = new PurchaseContainer();
			var supplier = _db.Contacts.Find(SupplierId);
			vm.SupplierData.SupplierId = SupplierId;
			vm.SupplierData.SupplierName = supplier.Name;
			vm.SupplierData.Phone = supplier.Phone1;
			vm.SupplierData.Balance = supplier.SupplierBalance;
			return vm;
		}

		public async Task SavePurchaseAsync(PurchaseContainer vm, IFormFile Invoicefile)
		{

			// Money Transactions
			// Add New Purchase 
			var purchase = _mapper.Map<Purchase>(vm.PurchaseSummary);
			purchase.SupplierId = vm.SupplierData.SupplierId;
			_db.Purchases.Add(purchase);
			await _db.SaveChangesAsync();
			var Contact = _db.Contacts.Find(vm.SupplierData.SupplierId);
			// Create Journal => 1-StoreAccounts 2- VAT-PurchaseAcc 3- SupplierAcc
			var TransId = await _journalManager.PurchaseJournalAsync(vm, Contact, Invoicefile);

			//// Update Balance Contacts "EGP" & Update Balance ContactBalanceInCurrency "Used Currency"
			var currency = _db.Currency.Find(vm.PurchaseSummary.CurrencyId);
			//  Get TotalAmount With Local Currency
			var LocalAmount = vm.PurchaseSummary.TotalAmountWithVAT * currency.Rate;
			// Update With Balance in Currecny
			_supplierBalanceManager.ManageSupplierBalanceInCurrencyAsync(Contact.Id, Contact.SupplierAccNum,
												currency.Id, vm.PurchaseSummary.TotalAmountWithVAT, true);
			// Update Contact Balance
			var BalanceAfter = await _supplierBalanceManager.UpdateSupplierBalanceAsync(Contact, LocalAmount, true);

			// SupplierTransacion (PurchaseId, TransId)
			await _supplierTransactionManager.PurchaseSupplierTransAsync(vm, purchase.Id, Contact.SupplierAccNum, TransId, BalanceAfter);

			// Store Transactions

			foreach (var item in vm.PurchaseDetails)
			{
				//Update StoreItem Balance And QTY
				var totalStoreItem = vm.PurchaseSummary.IsVAT ? item.QTY * item.UnitPrice * currency.Rate * 1.14M :
					item.QTY * item.UnitPrice * currency.Rate;

				var StoreItem = _db.StoreItems.Find(item.StoreItemId);
				StoreItem.Qty = StoreItem.Qty + item.QTY;
				StoreItem.Balance = totalStoreItem;
				_db.StoreItems.Update(StoreItem);

				//StoreTransaction
				var StoreTrans = new StoreTransaction();
				StoreTrans.StoreItemId = item.StoreItemId;
				StoreTrans.PurchaseId = purchase.Id;
				StoreTrans.UnitPrice = item.UnitPrice;
				StoreTrans.Qty = item.QTY;
				StoreTrans.StoreTransType = StoreTransType.Purchase;
				StoreTrans.QtyBalanceAfter = StoreItem.Qty;
				_db.StoreTransactions.Add(StoreTrans);

				//StoreItemBalanceDetails
				var ItemBalanceDetails = new StoreItemBalanceDetails();
				ItemBalanceDetails.StoreItemId = item.StoreItemId;
				ItemBalanceDetails.StoreTransactionId = StoreTrans.Id;
				ItemBalanceDetails.CurrentBalance = item.QTY;
				if (!string.IsNullOrEmpty(item.ExpiryDate))
					ItemBalanceDetails.ExpiryDate = item.ExpiryDate.ToEgyptionDate();
				_db.StoreItemBalanceDetails.Add(ItemBalanceDetails);

				//SN=> Insert
				if (StoreItem.WithSN)
				{
					var SN = item.SN.Split(',').ToList();
					foreach (var sn in SN)
					{
						var ItemWithSN = new StoreItemWithSN()
						{
							SerialNumber = sn,
							StoreItemId = item.StoreItemId,
							TransactionId = StoreTrans.Id
						};
						_db.StoreItemWithSN.Add(ItemWithSN);
					}
				}
			}
			await _db.SaveChangesAsync();
		}

		public async Task SaveReturnPurchaseAsync(PurchaseReturnBackContainer vm)
		{

			var Cur = _db.Currency.Find(vm.PurchaseDetails.CurrencyId);
			var TotalReturnedAmount = vm.PurchaseStoreDetails.Sum(x => x.ReturnedQTY * x.UnitPrice);
			decimal VATForReturned = 0;
			var OrginalPurchase = _db.Purchases.Find(vm.PurchaseId);
			if (OrginalPurchase.IsVAT)
			{
				VATForReturned = TotalReturnedAmount * 0.14M * Cur.Rate;
				TotalReturnedAmount = TotalReturnedAmount * 1.14M;
			}

			var TotalInLocal = TotalReturnedAmount * Cur.Rate;
			var Contact = _db.Contacts.Find(vm.PurchaseDetails.SupplierId);
			// Update Contact Balance
			var BalanceAfter = await _supplierBalanceManager.UpdateSupplierBalanceAsync(Contact, TotalInLocal, false);

			// Update With Balance in Currecny
			_supplierBalanceManager.ManageSupplierBalanceInCurrencyAsync(Contact.Id, Contact.SupplierAccNum,
												vm.PurchaseDetails.CurrencyId, TotalReturnedAmount, false);
			// Journal Transaction

			var TransId = await _journalManager.PurchaseReturnJournalAsync(vm, Contact, Cur, OrginalPurchase.IsVAT, TotalInLocal, TotalReturnedAmount);
			// Supplier Transaction

			await _supplierTransactionManager.SupplierReturnTransAsync(Contact, TotalInLocal, Cur.Id, TransId, BalanceAfter);

			// Store Transaction
			foreach (var item in vm.PurchaseStoreDetails)
			{
				//Update StoreItem Balance And QTY
				var totalStoreItem = OrginalPurchase.IsVAT ? item.ReturnedQTY * item.UnitPrice * Cur.Rate * 1.14M :
					item.ReturnedQTY * item.UnitPrice * Cur.Rate;

				var StoreItem = _db.StoreItems.Find(item.StoreItemId);
				StoreItem.Qty = StoreItem.Qty - item.ReturnedQTY;
				StoreItem.Balance = StoreItem.Balance - totalStoreItem;
				_db.StoreItems.Update(StoreItem);

				//StoreTransaction
				var StoreTrans = new StoreTransaction();
				StoreTrans.StoreItemId = item.StoreItemId;
				StoreTrans.PurchaseId = vm.PurchaseId;
				StoreTrans.UnitPrice = item.UnitPrice;
				StoreTrans.Qty = item.ReturnedQTY;
				StoreTrans.StoreTransType = StoreTransType.ReturnPurchase;
				StoreTrans.QtyBalanceAfter = StoreItem.Qty;
				_db.StoreTransactions.Add(StoreTrans);

				//StoreItemBalanceDetails
				var ItemBalanceDetails = new StoreItemBalanceDetails();
				ItemBalanceDetails.StoreItemId = item.StoreItemId;
				ItemBalanceDetails.StoreTransactionId = StoreTrans.Id;
				ItemBalanceDetails.CurrentBalance = item.ReturnedQTY;

				_db.StoreItemBalanceDetails.Add(ItemBalanceDetails);
			}
			await _db.SaveChangesAsync();
		}

		public PurchaseReturnBackContainer GetPurchaseData(int PurchaseId)
		{
			var vm = new PurchaseReturnBackContainer();
			vm.PurchaseId = PurchaseId;
			vm.PurchaseDetails = _db.Purchases.Include(x => x.SupplierDetails).Include(x => x.Currency)
									.Select(x => new PurchaseReturnBackDetails()
									{
										CurrencyAbbr = x.Currency.CurrencyAbbrev,
										CurrencyId = x.CurrencyId,
										InvoiceNum = x.InvoiceNum,
										PurchaseDate = x.PurchaseDate.ToString("dd/MM/yyyy"),
										SupplierId = x.SupplierId,
										SupplierName = x.SupplierDetails.Name,
										TotalAmount = x.TotalAmount,
										TotalAmountWithVAT = x.TotalAmountWithVAT,
										VATAmount = x.VATAmount
									}).FirstOrDefault();
			vm.PurchaseStoreDetails = _db.StoreTransactions.Where(x => x.PurchaseId == PurchaseId)
				.Include(x => x.StoreItem)
				.Select(x => new PurchaseStoreTransactions()
				{
					QTY = x.Qty,
					StoreItemName = x.StoreItem.Name,
					ReturnedQTY = 0,
					StoreItemId = x.StoreItemId,
					UnitPrice = x.UnitPrice
				}).ToList();
			return vm;
		}
	}



}

