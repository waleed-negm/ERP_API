using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using Application.BusinessLogic.PurchasesModule.Interfaces;
using Application.BusinessLogic.PurchasesModule.ViewModel.Expenses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.PurchasesModule.Services
{
	public class ExpensesManager
	{
		private readonly ApplicationDbContext _db;
		private readonly IAccountGenerator _accountGenerator;
		private readonly IMapper _mapper;
		private readonly ISupplierBalanceManager _supplierBalanceManager;
		private readonly ISupplierJournalsManager _supplierJournalsManager;
		private readonly ISupplierTransactionManager _supplierTransactionManager;

		public ExpensesManager(ApplicationDbContext db, IAccountGenerator accountGenerator,
			IMapper mapper, ISupplierBalanceManager supplierBalanceManager
			, ISupplierJournalsManager supplierJournalsManager, ISupplierTransactionManager supplierTransactionManager)
		{
			_db = db;
			_accountGenerator = accountGenerator;
			_mapper = mapper;
			_supplierBalanceManager = supplierBalanceManager;
			_supplierJournalsManager = supplierJournalsManager;
			_supplierTransactionManager = supplierTransactionManager;
		}

		public List<ExpenseItem> GetAllExpenseItems() =>
			_db.ExpenseItems.Include(x => x.ExpenseType).ToList();

		public void AddNewExpenseItem(ExpenseCreationVM vm)
		{

			using (var transaction = _db.Database.BeginTransaction())
			{
				try
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
					expense.AccNum = _accountGenerator.CreateNewAccount(account);
					_db.ExpenseItems.Add(expense);
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


		public void SaveNewExpense(ExpenseVM vm)
		{
			using (var transaction = _db.Database.BeginTransaction())
			{
				try
				{

					var Currency = _db.Currency.Find(vm.ExpenseDetails.CurrencyId);
					var ExpenseItem = _db.ExpenseItems.Find(vm.ExpenseDetails.ExpenseItemId);
					var supplier = _db.Contacts.Find(vm.ExpenseDetails.SupplierId);
					// Expense Summary
					var ES = _mapper.Map<ExpenseSummary>(vm.ExpenseDetails);
					ES.LocalAmount = vm.ExpenseDetails.Amount * Currency.Rate;
					_db.ExpenseSummary.Add(ES);
					_db.SaveChanges();

					// Supplier Payment
					var RestAmount = vm.ExpenseDetails.Amount - vm.PaymentDetails.PaymentAmount;
					// RestAmount = 0    => Journal Transaction 
					// RestAmount > 0    => Supplier Balances , Supplier Transaction , Jounral Transaction
					var TransId = _supplierJournalsManager.ExpenseJounral(vm, ExpenseItem.AccNum, supplier, Currency);
					if (RestAmount > 0)
					{
						var LocalAmount = RestAmount * Currency.Rate;

						// Update Balance Contact Table
						var BalanceAfter = _supplierBalanceManager.UpdateSupplierBalance(supplier, LocalAmount, true);
						// Update Balance With Currecny
						_supplierBalanceManager.UpdateBalanceInCurrency(supplier.Id, supplier.SupplierAccNum,
										   vm.ExpenseDetails.CurrencyId, RestAmount, true);

						_supplierTransactionManager.ExpenseSupplierTrans(vm, supplier.SupplierAccNum, TransId, BalanceAfter);
					}

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
