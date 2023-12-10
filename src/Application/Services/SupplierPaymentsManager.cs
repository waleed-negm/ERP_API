using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class SupplierPaymentsManager
	{
		private readonly IApplicationDbContext _db;
		private readonly ISupplierBalanceManager _supplierBalanceManager;
		private readonly ISupplierJournalsManager _supplierJournalsManager;
		private readonly ISupplierTransactionManager _supplierTransactionManager;
		private readonly NotesPayableManager _notesPayableManager;

		public SupplierPaymentsManager(IApplicationDbContext db, ISupplierBalanceManager supplierBalanceManager
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





		public async Task SaveSupplierPaymentAsync(SupplierPaymentContainer vm)
		{
			// Supplier Data
			var supplier = _db.Contacts.Find(vm.SupplierData.SupplierId);
			var Currency = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
			var LocalAmount = vm.PaymentDetails.PaymentAmount * Currency.Rate;

			// Update Balance Contact Table
			var BalanceAfter = await _supplierBalanceManager.UpdateSupplierBalanceAsync(supplier, LocalAmount, false);
			// Update Balance With Currecny
			await _supplierBalanceManager.UpdateBalanceInCurrencyAsync(supplier.Id, supplier.SupplierAccNum, vm.SelectedBalance.CurrencyId, vm.PaymentDetails.PaymentAmount, false);
			// Journal Transaction
			var TransId = await _supplierJournalsManager.SupplierPaymentJournalAsync(vm, supplier, LocalAmount);
			// Supplier Transaction
			_supplierTransactionManager.SupplierPaymentTransAsync(vm, LocalAmount, supplier.SupplierAccNum, TransId, BalanceAfter);

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
				await _notesPayableManager.SaveNewNPAsync(check);

				await _notesPayableManager.SaveNewNPHistoryAsync(check, TransId);
			}
			await _db.SaveChangesAsync();
		}
	}
}
