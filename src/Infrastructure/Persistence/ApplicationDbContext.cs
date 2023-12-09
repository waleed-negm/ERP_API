using System.Security.Claims;
using Domain.Entities;
using Domain.Entities.Auth;
using Domain.Entities.common;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		protected string _userName;
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor accessor) : base(options)
		{
			if (accessor.HttpContext != null)
			{
				_userName = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			}
		}

		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<SalaryBatch> SalaryBatches { get; set; }
		public DbSet<SalaryDetails> SalaryDetails { get; set; }
		public DbSet<Contacts> Contacts { get; set; }
		public DbSet<ContactBalanceInCurrency> ContactBalanceInCurrency { get; set; }
		public DbSet<NotesPayable> NotesPayables { get; set; }
		public DbSet<NotesPayableTransactionHistory> NotesPayableTransactionHistory { get; set; }
		public DbSet<Check> Check { get; set; }
		public DbSet<CheckHafza> CheckHafza { get; set; }
		public DbSet<CheckHistory> CheckHistory { get; set; }
		public DbSet<CheckLocation> CheckLocation { get; set; }
		public DbSet<CheckStatus> CheckStatus { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<UnitMeasure> UnitMeasures { get; set; }
		public DbSet<StoreItem> StoreItems { get; set; }
		public DbSet<StoreItemWithSN> StoreItemWithSN { get; set; }
		public DbSet<StoreTransaction> StoreTransactions { get; set; }
		public DbSet<StoreItemBalanceDetails> StoreItemBalanceDetails { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		public DbSet<SupplierTransaction> SupplierTransactions { get; set; }
		public DbSet<ExpenseType> expenseTypes { get; set; }
		public DbSet<ExpenseItem> ExpenseItems { get; set; }
		public DbSet<ExpenseSummary> ExpenseSummary { get; set; }
		public DbSet<Invoices> Invoices { get; set; }
		public DbSet<ClientTransaction> ClientTransactions { get; set; }
		public DbSet<AccountChart> AccountChart { get; set; }
		public DbSet<AccountChartCounter> AccountChartCounter { get; set; }
		public DbSet<Journal> Journal { get; set; }
		public DbSet<JournalDetails> JournalDetails { get; set; }
		public DbSet<FiniacialPeriod> FiniacialPeriods { get; set; }
		public DbSet<HistoricalBalance> HistoricalBalances { get; set; }
		public DbSet<Currency> Currency { get; set; }
		public DbSet<Branch> Branch { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			foreach (IMutableForeignKey relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
				relationship.DeleteBehavior = DeleteBehavior.NoAction;

			builder.Entity<ClientTransaction>()
				.HasOne(ct => ct.InvoiceDetails)
				.WithMany() // Assuming there is a one-to-many relationship
				.HasForeignKey(ct => ct.InvoiceNum) // Specify the foreign key property
				.HasPrincipalKey(inv => inv.InoviceNum);

			builder.Entity<JournalDetails>()
				.HasOne(ct => ct.Trans)
				.WithMany() // Assuming there is a one-to-many relationship
				.HasForeignKey(ct => ct.TransId) // Specify the foreign key property
				.HasPrincipalKey(inv => inv.TransId);

			builder.Entity<NotesPayableTransactionHistory>()
				.HasOne(ct => ct.ChkDetails)
				.WithMany() // Assuming there is a one-to-many relationship
				.HasForeignKey(ct => ct.ChkNum) // Specify the foreign key property
				.HasPrincipalKey(inv => inv.ChkNum);
			builder.Entity<AccountChart>().Property(a => a.IsDeleted).HasDefaultValue(false);

			builder.Entity<Department>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Employee>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<SalaryBatch>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<SalaryDetails>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Contacts>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<ContactBalanceInCurrency>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<NotesPayable>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<NotesPayableTransactionHistory>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Check>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<CheckHafza>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<CheckHistory>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<CheckLocation>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<CheckStatus>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Brand>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<ProductType>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<UnitMeasure>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<StoreItem>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<StoreItemWithSN>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<StoreTransaction>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<StoreItemBalanceDetails>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Purchase>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<SupplierTransaction>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<ExpenseType>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<ExpenseItem>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<ExpenseSummary>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Invoices>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<ClientTransaction>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<AccountChart>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<AccountChartCounter>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Journal>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<JournalDetails>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<FiniacialPeriod>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<HistoricalBalance>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Currency>().Property(a => a.IsDeleted).HasDefaultValue(false);
			builder.Entity<Branch>().Property(a => a.IsDeleted).HasDefaultValue(false);

			builder.Entity<Department>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Employee>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<SalaryBatch>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<SalaryDetails>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Contacts>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ContactBalanceInCurrency>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<NotesPayable>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<NotesPayableTransactionHistory>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Check>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckHafza>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckHistory>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckLocation>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckStatus>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Brand>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ProductType>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<UnitMeasure>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreItem>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreItemWithSN>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreTransaction>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreItemBalanceDetails>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Purchase>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<SupplierTransaction>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ExpenseType>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ExpenseItem>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ExpenseSummary>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Invoices>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ClientTransaction>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<AccountChart>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<AccountChartCounter>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Journal>().Property(a => a.Id).ValueGeneratedOnAdd();
			//builder.Entity<Journal>().Property(a => a.TransCount).valuegenerea();
			builder.Entity<JournalDetails>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<FiniacialPeriod>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<HistoricalBalance>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Currency>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Branch>().Property(a => a.Id).ValueGeneratedOnAdd();


			builder.Entity<Department>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Employee>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<SalaryBatch>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<SalaryDetails>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Contacts>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<ContactBalanceInCurrency>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<NotesPayable>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<NotesPayableTransactionHistory>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Check>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<CheckHafza>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<CheckHistory>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<CheckLocation>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<CheckStatus>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Brand>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<ProductType>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<UnitMeasure>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<StoreItem>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<StoreItemWithSN>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<StoreTransaction>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<StoreItemBalanceDetails>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Purchase>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<SupplierTransaction>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<ExpenseType>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<ExpenseItem>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<ExpenseSummary>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Invoices>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<ClientTransaction>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<AccountChart>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<AccountChartCounter>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Journal>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<JournalDetails>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<FiniacialPeriod>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<HistoricalBalance>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Currency>().HasQueryFilter(b => !b.IsDeleted);
			builder.Entity<Branch>().HasQueryFilter(b => !b.IsDeleted);
			#region Seed Check Settings
			builder.Entity<Employee>().HasData(
				new { Id = (long)1, BasicSalary = 5000M, DepartmentId = (long)1, InsuranceSalary = 2500M, Name = "Ahmed", Phone = "0124558880", Title = "HR Manager" },
				new { Id = (long)2, BasicSalary = 10000M, DepartmentId = (long)2, InsuranceSalary = 6000M, Name = "Mohamed", Phone = "0124558881", Title = "IT Manager" },
				new { Id = (long)3, BasicSalary = 20000M, DepartmentId = (long)2, InsuranceSalary = 7000M, Name = "Peter", Phone = "0124558882", Title = "Finance Manager" }
				);
			builder.Entity<SalaryBatch>().HasData(new { Id = (long)1, BatchMonth = 12, BatchYear = 2020 });
			builder.Entity<CheckLocation>().HasData(
				new { Id = (long)1, CheckLocationEN = "Safe", CheckLocationAR = "الخزنة", IsDefualt = true },
				new { Id = (long)2, CheckLocationEN = "Bank", CheckLocationAR = "البنك", IsDefualt = false },
				new { Id = (long)3, CheckLocationEN = "BankCollected", CheckLocationAR = "محصل", IsDefualt = false },
				new { Id = (long)4, CheckLocationEN = "Client", CheckLocationAR = "مع العميل", IsDefualt = false }
				);
			builder.Entity<CheckStatus>().HasData(
				new { Id = (long)1, CheckStatusEN = "Under Collection", CheckStatusAR = "تحت التحصيل", IsDefault = true },
				new { Id = (long)2, CheckStatusEN = "Collected", CheckStatusAR = "محصل", IsDefault = true },
				new { Id = (long)3, CheckStatusEN = "Bounced", CheckStatusAR = "مرتد", IsDefault = true }
				);
			#endregion
			#region seeding
			builder.Entity<Branch>().HasData(new { Id = (long)1, BranchName = "Main" });
			builder.Entity<Department>().HasData(new { Id = (long)1, DepartmentName = "IT" }, new Department() { Id = 2, DepartmentName = "HR" });
			builder.Entity<ExpenseType>().HasData(new { Id = (long)1, ExpenseTypeName = "Managment Expenses" }, new { Id = (long)2, ExpenseTypeName = "Banking Expenses" });
			builder.Entity<Currency>().HasData(
				new { Id = (long)1, CurrencyName = "Egyptain Pound", CurrencyNameAr = "جنية مصر", CurrencyAbbrev = "EGP", Rate = 1M, IsDefault = true },
				new { Id = (long)2, CurrencyName = "American Dollar", CurrencyNameAr = "دولار أمريكي", CurrencyAbbrev = "USD", Rate = 16.00M, IsDefault = false }
				);
			builder.Entity<FiniacialPeriod>().HasData(new { Id = (long)1, YearName = "2020-2021", IsActive = true });
			#endregion
			#region Seeding AccountChartCounter
			builder.Entity<AccountChartCounter>().HasData(
			new AccountChartCounter()
			{
				Id = 1,
				AccountType = "Buildings",
				AccountCategory = AccountCategoryEnum.FixedAsset,
				ParentAccNum = "1110000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 2,
				AccountType = "Machines And Equipments",
				AccountCategory = AccountCategoryEnum.FixedAsset,
				ParentAccNum = "1120000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 3,
				AccountType = "Furnitiures",
				AccountCategory = AccountCategoryEnum.FixedAsset,
				ParentAccNum = "1130000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 4,
				AccountType = "Safe",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1210000000",
				Count = 1,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 5,
				AccountType = "Bank",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1220000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 6,
				AccountType = "Client",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1230000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 7,
				AccountType = "Check",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1240000000",
				Count = 3,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 8,
				AccountType = "Store",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1250000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 9,
				AccountType = "Custody",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1261000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 10,
				AccountType = "StaffAdvances",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1262000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 11,
				AccountType = "SupplierAdvances",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1263000000",
				Count = 0
			},
			new AccountChartCounter()
			{
				Id = 12,
				AccountType = "OtherCurrentAsset",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1269000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 13,
				AccountType = "NotePayable",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2170000000",
				Count = 1,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 14,
				AccountType = "Suppliers",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2210000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 15,
				AccountType = "Taxes",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2220000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 16,
				AccountType = "Creditors",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2230000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 17,
				AccountType = "Accrued Expenses",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2240000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 18,
				AccountType = "Advances Income",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2250000000",
				Count = 1,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 19,
				AccountType = "Income",
				AccountCategory = AccountCategoryEnum.Income,
				ParentAccNum = "3110000000",
				Count = 0,
				BalanceSheet = false,
				IncomeStatement = true
			},
			new AccountChartCounter()
			{
				Id = 20,
				AccountType = "Expense",
				AccountCategory = AccountCategoryEnum.Expnese,
				ParentAccNum = "4111000000",
				Count = 0,
				BalanceSheet = false,
				IncomeStatement = true
			},
			new AccountChartCounter()
			{
				Id = 21,
				AccountType = "Purchases",
				AccountCategory = AccountCategoryEnum.StorePurchase,
				ParentAccNum = "4112000000",
				Count = 0,
				BalanceSheet = false,
				IncomeStatement = true
			},
			new AccountChartCounter()
			{
				Id = 22,
				AccountType = "Owners",
				AccountCategory = AccountCategoryEnum.OwnerEquity,
				ParentAccNum = "5110000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 23,
				AccountType = "OwnerWithdraw",
				AccountCategory = AccountCategoryEnum.OwnerEquity,
				ParentAccNum = "5120000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			},
			new AccountChartCounter()
			{
				Id = 24,
				AccountType = "OtherCurrentLiabilties",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2260000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			}
						 );
			#endregion
			#region Seeding AccountChart
			builder.Entity<AccountChart>().HasData(
					 new
					 {
						 AccountName = "Buildings",
						 AccountNameAr = "مباني",
						 AccNum = "1110000000",
						 AccTypeId = (long)1,
						 IsParent = true,
						 CurrencyId = (long)1,
						 Balance = 0m,
						 BranchId = (long)1,
						 ParentAcNum = "",
						 IsActive = true,
						 AccountNature = AccountNatureEnum.Debit
					 },
					 new
					 {
						 AccountName = "Machines And Equipments",
						 AccountNameAr = "أجهزة و معدات",
						 AccNum = "1120000000",
						 AccTypeId = (long)2,
						 IsParent = true,
						 CurrencyId = (long)1,
						 Balance = 0m,
						 BranchId = (long)1,
						 ParentAcNum = "",
						 IsActive = true,
						 AccountNature = AccountNatureEnum.Debit
					 },
					  new
					  {
						  AccountName = "Furnitiures",
						  AccountNameAr = "أثاث",
						  AccNum = "1130000000",
						  AccTypeId = (long)3,
						  IsParent = true,
						  CurrencyId = (long)1,
						  Balance = 0m,
						  BranchId = (long)1,
						  ParentAcNum = "",
						  IsActive = true,
						  AccountNature = AccountNatureEnum.Debit
					  },
					new
					{
						AccountName = "Safes",
						AccountNameAr = "خزن",
						AccNum = "1210000000",
						AccTypeId = (long)4,
						IsParent = true,
						CurrencyId = (long)1,
						Balance = 0m,
						BranchId = (long)1,
						ParentAcNum = "",
						IsActive = true,
						AccountNature = AccountNatureEnum.Debit
					},
					 new
					 {
						 AccountName = "Banks",
						 AccountNameAr = "بنوك",
						 AccNum = "1220000000",
						 AccTypeId = (long)5,
						 IsParent = true,
						 CurrencyId = (long)1,
						 Balance = 0m,
						 BranchId = (long)1,
						 ParentAcNum = "",
						 IsActive = true,
						 AccountNature = AccountNatureEnum.Debit
					 },
					 new
					 {
						 AccountName = "Clients",
						 AccountNameAr = "عملاء",
						 AccNum = "1230000000",
						 AccTypeId = (long)6,
						 IsParent = true,
						 CurrencyId = (long)1,
						 Balance = 0m,
						 BranchId = (long)1,
						 ParentAcNum = "",
						 IsActive = true,
						 AccountNature = AccountNatureEnum.Debit
					 },
					 new
					 {
						 AccountName = "Clients",
						 AccountNameAr = "عملاء",
						 AccNum = "1230000001",
						 AccTypeId = (long)6,
						 IsParent = true,
						 CurrencyId = (long)1,
						 Balance = 0m,
						 BranchId = (long)1,
						 ParentAcNum = "1230000000",
						 IsActive = true,
						 AccountNature = AccountNatureEnum.Debit
					 },
					  new
					  {
						  AccountName = "Checks",
						  AccountNameAr = "شيكات",
						  AccNum = "1240000000",
						  AccTypeId = (long)7,
						  IsParent = true,
						  CurrencyId = (long)1,
						  Balance = 0m,
						  BranchId = (long)1,
						  ParentAcNum = "",
						  IsActive = true,
						  AccountNature = AccountNatureEnum.Debit
					  },
					   new
					   {
						   AccountName = "Checks In Safe",
						   AccountNameAr = "شيكات في الخزنة",
						   AccNum = "1240000001",
						   AccTypeId = (long)7,
						   IsParent = false,
						   CurrencyId = (long)1,
						   Balance = 0m,
						   BranchId = (long)1,
						   ParentAcNum = "1240000000",
						   IsActive = true,
						   AccountNature = AccountNatureEnum.Debit
					   },
						new
						{
							AccountName = "Checks In Bank",
							AccountNameAr = "شيكات في البنك",
							AccNum = "1240000002",
							AccTypeId = (long)7,
							IsParent = false,
							CurrencyId = (long)1,
							Balance = 0m,
							BranchId = (long)1,
							ParentAcNum = "1240000000",
							IsActive = true,
							AccountNature = AccountNatureEnum.Debit
						},
						new
						{
							AccountName = "Bounced Checks",
							AccountNameAr = "شيكات مرتدة",
							AccNum = "1240000003",
							AccTypeId = (long)7,
							IsParent = false,
							CurrencyId = (long)1,
							Balance = 0m,
							BranchId = (long)1,
							ParentAcNum = "1240000000",
							IsActive = true,
							AccountNature = AccountNatureEnum.Debit
						},
					  new
					  {
						  AccountName = "Store",
						  AccountNameAr = "مخزن",
						  AccNum = "1250000000",
						  AccTypeId = (long)8,
						  IsParent = true,
						  CurrencyId = (long)1,
						  Balance = 0m,
						  BranchId = (long)1,
						  ParentAcNum = "",
						  IsActive = true,
						  AccountNature = AccountNatureEnum.Debit
					  },
						new
						{
							AccountName = "Custody",
							AccountNameAr = "عهد",
							AccNum = "1261000000",
							AccTypeId = (long)9,
							IsParent = true,
							CurrencyId = (long)1,
							Balance = 0m,
							BranchId = (long)1,
							ParentAcNum = "",
							IsActive = true,
							AccountNature = AccountNatureEnum.Debit
						},
						new
						{
							AccountName = "StaffAdvances",
							AccountNameAr = "سلف",
							AccNum = "1262000000",
							AccTypeId = (long)10,
							IsParent = true,
							CurrencyId = (long)1,
							Balance = 0m,
							BranchId = (long)1,
							ParentAcNum = "",
							IsActive = true,
							AccountNature = AccountNatureEnum.Debit
						},
						new
						{
							AccountName = "Suppliers Advances",
							AccountNameAr = "دفعات مقدمة للموردين",
							AccNum = "1263000000",
							AccTypeId = (long)11,
							IsParent = true,
							CurrencyId = (long)1,
							Balance = 0m,
							BranchId = (long)1,
							ParentAcNum = "",
							IsActive = true,
							AccountNature = AccountNatureEnum.Debit
						},
						 new
						 {
							 AccountName = "OtherCurrentAsset",
							 AccountNameAr = "أصول متداولة أخري",
							 AccNum = "1269000000",
							 AccTypeId = (long)12,
							 IsParent = true,
							 CurrencyId = (long)1,
							 Balance = 0m,
							 BranchId = (long)1,
							 ParentAcNum = "",
							 IsActive = true,
							 AccountNature = AccountNatureEnum.Debit
						 },
						   new
						   {
							   AccountName = "NotePayable",
							   AccountNameAr = "شيكات موردين",
							   AccNum = "2170000000",
							   AccTypeId = (long)13,
							   IsParent = true,
							   CurrencyId = (long)1,
							   Balance = 0m,
							   BranchId = (long)1,
							   ParentAcNum = "",
							   IsActive = true,
							   AccountNature = AccountNatureEnum.Credit
						   },
						   new
						   {
							   AccountName = "NotePayable",
							   AccountNameAr = "شيكات موردين",
							   AccNum = "2170000001",
							   AccTypeId = (long)13,
							   IsParent = true,
							   CurrencyId = (long)1,
							   Balance = 0m,
							   BranchId = (long)1,
							   ParentAcNum = "2170000000",
							   IsActive = false,
							   AccountNature = AccountNatureEnum.Credit
						   },
						  new
						  {
							  AccountName = "Suppliers",
							  AccountNameAr = "موردين",
							  AccNum = "2210000000",
							  AccTypeId = (long)14,
							  IsParent = true,
							  CurrencyId = (long)1,
							  Balance = 0m,
							  BranchId = (long)1,
							  ParentAcNum = "",
							  IsActive = true,
							  AccountNature = AccountNatureEnum.Credit
						  },
						   new
						   {
							   AccountName = "Taxes",
							   AccountNameAr = "ضرائب",
							   AccNum = "2220000000",
							   AccTypeId = (long)15,
							   IsParent = true,
							   CurrencyId = (long)1,
							   Balance = 0m,
							   BranchId = (long)1,
							   ParentAcNum = "",
							   IsActive = true,
							   AccountNature = AccountNatureEnum.Credit
						   },
						   new
						   {
							   AccountName = "Creditors",
							   AccountNameAr = "دائنون",
							   AccNum = "2230000000",
							   AccTypeId = (long)16,
							   IsParent = true,
							   CurrencyId = (long)1,
							   Balance = 0m,
							   BranchId = (long)1,
							   ParentAcNum = "",
							   IsActive = true,
							   AccountNature = AccountNatureEnum.Credit
						   },
							new
							{
								AccountName = "Accrued Expenses",
								AccountNameAr = "مصروفات مستحقة",
								AccNum = "2240000000",
								AccTypeId = (long)17,
								IsParent = true,
								CurrencyId = (long)1,
								Balance = 0m,
								BranchId = (long)1,
								ParentAcNum = "",
								IsActive = true,
								AccountNature = AccountNatureEnum.Credit
							},
							 new
							 {
								 AccountName = "Advances Income",
								 AccountNameAr = "ايرادات مقدمة",
								 AccNum = "2250000000",
								 AccTypeId = (long)18,
								 IsParent = true,
								 CurrencyId = (long)1,
								 Balance = 0m,
								 BranchId = (long)1,
								 ParentAcNum = "",
								 IsActive = true,
								 AccountNature = AccountNatureEnum.Credit
							 },
							  new
							  {
								  AccountName = "Advances Income",
								  AccountNameAr = "ايرادات مقدمة",
								  AccNum = "2250000001",
								  AccTypeId = (long)18,
								  IsParent = true,
								  CurrencyId = (long)1,
								  Balance = 0m,
								  BranchId = (long)1,
								  ParentAcNum = "2250000000",
								  IsActive = false,
								  AccountNature = AccountNatureEnum.Credit
							  },
							   new
							   {
								   AccountName = "Other Current Liabilities",
								   AccountNameAr = "التزمات متداولة أخري",
								   AccNum = "2260000000",
								   AccTypeId = (long)24,
								   IsParent = true,
								   CurrencyId = (long)1,
								   Balance = 0m,
								   BranchId = (long)1,
								   ParentAcNum = "",
								   IsActive = true,
								   AccountNature = AccountNatureEnum.Credit
							   },
							new
							{
								AccountName = "Incomes",
								AccountNameAr = "إيرادات",
								AccNum = "3110000000",
								AccTypeId = (long)19,
								IsParent = true,
								CurrencyId = (long)1,
								Balance = 0m,
								BranchId = (long)1,
								ParentAcNum = "",
								IsActive = true,
								AccountNature = AccountNatureEnum.Credit
							},
							new
							{
								AccountName = "Expenses",
								AccountNameAr = "مصروفات",
								AccNum = "4111000000",
								AccTypeId = (long)20,
								IsParent = true,
								CurrencyId = (long)1,
								Balance = 0m,
								BranchId = (long)1,
								ParentAcNum = "",
								IsActive = true,
								AccountNature = AccountNatureEnum.Debit
							},
							new
							{
								AccountName = "Purchases",
								AccountNameAr = "مشتريات",
								AccNum = "4112000000",
								AccTypeId = (long)21,
								IsParent = true,
								CurrencyId = (long)1,
								Balance = 0m,
								BranchId = (long)1,
								ParentAcNum = "",
								IsActive = true,
								AccountNature = AccountNatureEnum.Debit
							},
							  new
							  {
								  AccountName = "Capital",
								  AccountNameAr = "رأس المال",
								  AccNum = "5110000000",
								  AccTypeId = (long)22,
								  IsParent = true,
								  CurrencyId = (long)1,
								  Balance = 0m,
								  BranchId = (long)1,
								  ParentAcNum = "",
								  IsActive = true,
								  AccountNature = AccountNatureEnum.Credit
							  },

							   new
							   {
								   AccountName = "WithDrawls",
								   AccountNameAr = "مسحوبات",
								   AccNum = "5120000000",
								   AccTypeId = (long)23,
								   IsParent = true,
								   CurrencyId = (long)1,
								   Balance = 0m,
								   BranchId = (long)1,
								   ParentAcNum = "",
								   IsActive = true,
								   AccountNature = AccountNatureEnum.Credit
							   }
				);
			#endregion
		}
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (EntityEntry<BaseModel> entity in ChangeTracker.Entries<BaseModel>())
			{
				switch (entity.State)
				{
					case EntityState.Added:
						entity.Entity.CreatedOn = DateTime.Now;
						entity.Entity.CreatedBy = _userName;
						entity.Entity.IsDeleted = false;
						break;
					case EntityState.Modified:
						entity.Entity.ModifiedOn = DateTime.Now;
						entity.Entity.ModifiedBy = _userName;
						break;
					case EntityState.Deleted:
						entity.State = EntityState.Modified;
						entity.Entity.IsDeleted = true;
						entity.Entity.ModifiedOn = DateTime.Now;
						entity.Entity.ModifiedBy = _userName;
						break;
				}
			}
			foreach (EntityEntry<ApplicationUser> entity in ChangeTracker.Entries<ApplicationUser>())
			{
				switch (entity.State)
				{
					case EntityState.Added:
						entity.Entity.CreatedOn = DateTime.Now;
						entity.Entity.CreatedBy = _userName;
						entity.Entity.IsDeleted = false;
						break;
					case EntityState.Modified:
						entity.Entity.ModifiedOn = DateTime.Now;
						entity.Entity.ModifiedBy = _userName;
						break;
					case EntityState.Deleted:
						entity.State = EntityState.Modified;
						entity.Entity.IsDeleted = true;
						entity.Entity.ModifiedOn = DateTime.Now;
						entity.Entity.ModifiedBy = _userName;
						break;
				}
			}
			return await base.SaveChangesAsync(cancellationToken);

		}
	}
}
