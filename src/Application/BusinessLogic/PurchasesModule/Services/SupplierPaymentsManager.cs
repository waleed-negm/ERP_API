using Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.Services;
using Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.Interfaces;
using Application.BusinessLogic.PurchasesModule.ViewModel;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.PurchasesModule.Services
{
	public class SupplierPaymentsManager
	{
		private readonly ApplicationDbContext _db;
		private readonly ISupplierBalanceManager _supplierBalanceManager;
		private readonly ISupplierJournalsManager _supplierJournalsManager;
		private readonly ISupplierTransactionManager _supplierTransactionManager;
		private readonly NotesPayableManager _notesPayableManager;

		public SupplierPaymentsManager(ApplicationDbContext db, ISupplierBalanceManager supplierBalanceManager
			, ISupplierJournalsManager supplierJournalsManager, ISupplierTransactionManager supplierTransactionManager
			, NotesPayableManager notesPayableManager)
		{
			_db = db;
			_supplierBalanceManager = supplierBalanceManager;
			_supplierJournalsManager = supplierJournalsManager;
			_supplierTransactionManager = supplierTransactionManager;
			_notesPayableManager = notesPayableManager;

		}

		public SupplierPaymentContainer NewPayment(int SupplierId)
		{
			var vm = new SupplierPaymentContainer();
			var supplier = _db.Contacts.Find(SupplierId);
			vm.SupplierData.SupplierId = SupplierId;
			vm.SupplierData.SupplierName = supplier.Name;
			vm.SupplierData.Phone = supplier.Phone1;
			vm.SupplierData.Balance = supplier.SupplierBalance;

			vm.SupplierBalanceDetails = _db.ContactBalanceInCurrency.Include(x => x.Currency)
										.Where(x => x.ContactId == SupplierId
										&& x.AccNum == supplier.SupplierAccNum)
										.Select(x => new SupplierBalanceDetails()
										{
											Amount = x.Balance,
											CurrencyAbbr = x.Currency.CurrencyAbbrev,
											CurrencyId = x.CurrencyId,
											LocalAmount = x.Balance * x.Currency.Rate,
											Rate = x.Currency.Rate
										}).ToList();

			return vm;
		}





		public void SaveSupplierPayment(SupplierPaymentContainer vm)
		{

			using (var transaction = _db.Database.BeginTransaction())
			{
				try
				{
					// Supplier Data
					var supplier = _db.Contacts.Find(vm.SupplierData.SupplierId);
					var Currency = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
					var LocalAmount = vm.PaymentDetails.PaymentAmount * Currency.Rate;

					// Update Balance Contact Table
					var BalanceAfter = _supplierBalanceManager.UpdateSupplierBalance(supplier, LocalAmount, false);
					// Update Balance With Currecny
					_supplierBalanceManager.UpdateBalanceInCurrency(supplier.Id, supplier.SupplierAccNum,
									   vm.SelectedBalance.CurrencyId, vm.PaymentDetails.PaymentAmount, false);
					// Journal Transaction
					var TransId = _supplierJournalsManager.SupplierPaymentJournal(vm, supplier, LocalAmount);
					// Supplier Transaction
					_supplierTransactionManager.SupplierPaymentTrans(vm, LocalAmount,
						supplier.SupplierAccNum, TransId, BalanceAfter);

					// If NP (Supplier Check) Add Check and its history
					if (vm.PaymentDetails.PaymentMethod == SupplierPaymentMethod.Check)
					{
						var check = new NotesPayableCreationVM()
						{
							AmountForgin = vm.PaymentDetails.PaymentAmount,
							AmountLocal = LocalAmount,
							BankAccountNum = vm.PaymentDetails.BankAccNum,
							ChkNum = vm.PaymentDetails.CheckNum,
							CurrencyId = vm.SelectedBalance.CurrencyId,
							DueDate = vm.PaymentDetails.PaymentDueDate,
							WritingDate = vm.PaymentDetails.WritingDate,
							SupplierId = vm.SupplierData.SupplierId
						};
						_notesPayableManager.SaveNewNP(check);

						_notesPayableManager.SaveNewNPHistory(check, TransId);
					}
					_db.SaveChanges();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					var error = ex.Message;
					transaction.Rollback();
				}
			}



		}
	}
}
