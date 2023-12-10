using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class ExpensesManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IAccountGenerator _accountGenerator;
		private readonly IMapper _mapper;
		private readonly ISupplierBalanceManager _supplierBalanceManager;
		private readonly ISupplierJournalsManager _supplierJournalsManager;
		private readonly ISupplierTransactionManager _supplierTransactionManager;

		public ExpensesManager(IApplicationDbContext db, IAccountGenerator accountGenerator, IMapper mapper, ISupplierBalanceManager supplierBalanceManager, ISupplierJournalsManager supplierJournalsManager, ISupplierTransactionManager supplierTransactionManager)
		{
			_db = db;
			_accountGenerator = accountGenerator;
			_mapper = mapper;
			_supplierBalanceManager = supplierBalanceManager;
			_supplierJournalsManager = supplierJournalsManager;
			_supplierTransactionManager = supplierTransactionManager;
		}

		public List<ExpenseItem> GetAllExpenseItems() => _db.ExpenseItems.Include(x => x.ExpenseType).ToList();

		public async Task AddNewExpenseItemAsync(ExpenseCreationVM vm)
		{
			var expense = new ExpenseItem();
			expense.ExpenseName = vm.ExpenseName;
			expense.ExpenseTypeId = vm.ExpenseTypeId;
			var account = new CreateAccountVM();
			account.AccountName = vm.ExpenseName;
			account.AccountNameAr = vm.ExpenseName;
			account.AccTypeId = 20;
			account.BranchId = 1;
			account.CurrencyId = 1;
			expense.AccNum = await _accountGenerator.CreateNewAccountAsync(account);
			_db.ExpenseItems.Add(expense);
			await _db.SaveChangesAsync();
		}

		public async Task SaveNewExpenseAsync(ExpenseVM vm)
		{
			var Currency = _db.Currency.Find(vm.ExpenseDetails.CurrencyId);
			var ExpenseItem = _db.ExpenseItems.Find(vm.ExpenseDetails.ExpenseItemId);
			var supplier = _db.Contacts.Find(vm.ExpenseDetails.SupplierId);
			// Expense Summary
			var ES = _mapper.Map<ExpenseSummary>(vm.ExpenseDetails);
			ES.LocalAmount = vm.ExpenseDetails.Amount * Currency.Rate;
			_db.ExpenseSummary.Add(ES);
			await _db.SaveChangesAsync();

			// Supplier Payment
			var RestAmount = vm.ExpenseDetails.Amount - vm.PaymentDetails.PaymentAmount;
			// RestAmount = 0    => Journal Transaction 
			// RestAmount > 0    => Supplier Balances , Supplier Transaction , Jounral Transaction
			var TransId = await _supplierJournalsManager.ExpenseJounralAsync(vm, ExpenseItem.AccNum, supplier, Currency);
			if (RestAmount > 0)
			{
				var LocalAmount = RestAmount * Currency.Rate;
				// Update Balance Contact Table
				var BalanceAfter = await _supplierBalanceManager.UpdateSupplierBalanceAsync(supplier, LocalAmount, true);
				// Update Balance With Currecny
				await _supplierBalanceManager.UpdateBalanceInCurrencyAsync(supplier.Id, supplier.SupplierAccNum, vm.ExpenseDetails.CurrencyId, RestAmount, true);
				await _supplierTransactionManager.ExpenseSupplierTransAsync(vm, supplier.SupplierAccNum, TransId, BalanceAfter);
			}
		}
	}
}
