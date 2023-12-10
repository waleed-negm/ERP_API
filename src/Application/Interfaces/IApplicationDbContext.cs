using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<Department> Departments { get; }
		DbSet<Employee> Employees { get; }
		DbSet<SalaryBatch> SalaryBatches { get; }
		DbSet<SalaryDetails> SalaryDetails { get; }
		DbSet<Contacts> Contacts { get; }
		DbSet<ContactBalanceInCurrency> ContactBalanceInCurrency { get; }
		DbSet<NotesPayable> NotesPayables { get; }
		DbSet<NotesPayableTransactionHistory> NotesPayableTransactionHistory { get; }
		DbSet<Check> Check { get; }
		DbSet<CheckHafza> CheckHafza { get; }
		DbSet<CheckHistory> CheckHistory { get; }
		DbSet<CheckLocation> CheckLocation { get; }
		DbSet<CheckStatus> CheckStatus { get; }
		DbSet<Brand> Brands { get; }
		DbSet<ProductType> ProductTypes { get; }
		DbSet<UnitMeasure> UnitMeasures { get; }
		DbSet<StoreItem> StoreItems { get; }
		DbSet<StoreItemWithSN> StoreItemWithSN { get; }
		DbSet<StoreTransaction> StoreTransactions { get; }
		DbSet<StoreItemBalanceDetails> StoreItemBalanceDetails { get; }
		DbSet<Purchase> Purchases { get; }
		DbSet<SupplierTransaction> SupplierTransactions { get; }
		DbSet<ExpenseType> expenseTypes { get; }
		DbSet<ExpenseItem> ExpenseItems { get; }
		DbSet<ExpenseSummary> ExpenseSummary { get; }
		DbSet<Invoices> Invoices { get; }
		DbSet<ClientTransaction> ClientTransactions { get; }
		DbSet<AccountChart> AccountChart { get; }
		DbSet<AccountChartCounter> AccountChartCounter { get; }
		DbSet<Journal> Journal { get; }
		DbSet<JournalDetails> JournalDetails { get; }
		DbSet<FiniacialPeriod> FiniacialPeriods { get; }
		DbSet<HistoricalBalance> HistoricalBalances { get; }
		DbSet<Currency> Currency { get; }
		DbSet<Branch> Branch { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
