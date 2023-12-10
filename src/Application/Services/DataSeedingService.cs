using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
	public class DataSeedingService : IDataSeedingService
	{
		private readonly IApplicationDbContext _dbContext;
		public DataSeedingService(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task SeedDataAsync()
		{

			var itDep = new Department() { DepartmentName = "IT" };
			var hrDep = new Department() { DepartmentName = "HR" };
			await _dbContext.Departments.AddRangeAsync(itDep, hrDep);

			await _dbContext.Employees.AddRangeAsync(
				new Employee() { BasicSalary = 5000M, Department = itDep, InsuranceSalary = 2500M, Name = "Ahmed", Phone = "0124558880", Title = "HR Manager" },
				new Employee() { BasicSalary = 10000M, Department = hrDep, InsuranceSalary = 6000M, Name = "Mohamed", Phone = "0124558881", Title = "IT Manager" },
				new Employee() { BasicSalary = 20000M, Department = hrDep, InsuranceSalary = 7000M, Name = "Peter", Phone = "0124558882", Title = "Finance Manager" }
				);

			await _dbContext.SalaryBatches.AddRangeAsync(new SalaryBatch() { BatchMonth = 12, BatchYear = 2020 });

			await _dbContext.CheckLocation.AddRangeAsync(
				new CheckLocation() { CheckLocationEN = "Safe", CheckLocationAR = "الخزنة", IsDefualt = true },
				new CheckLocation() { CheckLocationEN = "Bank", CheckLocationAR = "البنك", IsDefualt = false },
				new CheckLocation() { CheckLocationEN = "BankCollected", CheckLocationAR = "محصل", IsDefualt = false },
				new CheckLocation() { CheckLocationEN = "Client", CheckLocationAR = "مع العميل", IsDefualt = false }
				);

			await _dbContext.CheckStatus.AddRangeAsync(
				new CheckStatus { CheckStatusEN = "Under Collection", CheckStatusAR = "تحت التحصيل", IsDefault = true },
				new CheckStatus { CheckStatusEN = "Collected", CheckStatusAR = "محصل", IsDefault = true },
				new CheckStatus { CheckStatusEN = "Bounced", CheckStatusAR = "مرتد", IsDefault = true }
				);

			var mainBranch = new Branch() { BranchName = "Main" };
			await _dbContext.Branch.AddRangeAsync(mainBranch);

			await _dbContext.expenseTypes.AddRangeAsync(new ExpenseType() { ExpenseTypeName = "Managment Expenses" }, new ExpenseType() { ExpenseTypeName = "Banking Expenses" });

			var egp = new Currency() { CurrencyName = "Egyptain Pound", CurrencyNameAr = "جنية مصر", CurrencyAbbrev = "EGP", Rate = 1M, IsDefault = true };
			var dollar = new Currency() { CurrencyName = "American Dollar", CurrencyNameAr = "دولار أمريكي", CurrencyAbbrev = "USD", Rate = 16.00M, IsDefault = false };
			await _dbContext.Currency.AddRangeAsync(egp, dollar);

			await _dbContext.FiniacialPeriods.AddRangeAsync(new FiniacialPeriod() { YearName = "2020-2021", IsActive = true });

			var accCharCounter1 = new AccountChartCounter()
			{
				AccountType = "Buildings",
				AccountCategory = AccountCategoryEnum.FixedAsset,
				ParentAccNum = "1110000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter2 = new AccountChartCounter()
			{
				AccountType = "Machines And Equipments",
				AccountCategory = AccountCategoryEnum.FixedAsset,
				ParentAccNum = "1120000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter3 = new AccountChartCounter()
			{
				AccountType = "Furnitiures",
				AccountCategory = AccountCategoryEnum.FixedAsset,
				ParentAccNum = "1130000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter4 = new AccountChartCounter()
			{
				AccountType = "Safe",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1210000000",
				Count = 1,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter5 = new AccountChartCounter()
			{
				AccountType = "Bank",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1220000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter6 = new AccountChartCounter()
			{
				AccountType = "Client",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1230000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter7 = new AccountChartCounter()
			{
				AccountType = "Check",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1240000000",
				Count = 3,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter8 = new AccountChartCounter()
			{
				AccountType = "Store",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1250000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter9 = new AccountChartCounter()
			{
				AccountType = "Custody",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1261000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter10 = new AccountChartCounter()
			{
				AccountType = "StaffAdvances",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1262000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter11 = new AccountChartCounter()
			{
				AccountType = "SupplierAdvances",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1263000000",
				Count = 0
			};
			var accCharCounter12 = new AccountChartCounter()
			{
				AccountType = "OtherCurrentAsset",
				AccountCategory = AccountCategoryEnum.CurrentAsset,
				ParentAccNum = "1269000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter13 = new AccountChartCounter()
			{
				AccountType = "NotePayable",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2170000000",
				Count = 1,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter14 = new AccountChartCounter()
			{
				AccountType = "Suppliers",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2210000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter15 = new AccountChartCounter()
			{
				AccountType = "Taxes",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2220000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter16 = new AccountChartCounter()
			{
				AccountType = "Creditors",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2230000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter17 = new AccountChartCounter()
			{
				AccountType = "Accrued Expenses",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2240000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter18 = new AccountChartCounter()
			{
				AccountType = "Advances Income",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2250000000",
				Count = 1,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter19 = new AccountChartCounter()
			{
				AccountType = "Income",
				AccountCategory = AccountCategoryEnum.Income,
				ParentAccNum = "3110000000",
				Count = 0,
				BalanceSheet = false,
				IncomeStatement = true
			};
			var accCharCounter20 = new AccountChartCounter()
			{
				AccountType = "Expense",
				AccountCategory = AccountCategoryEnum.Expnese,
				ParentAccNum = "4111000000",
				Count = 0,
				BalanceSheet = false,
				IncomeStatement = true
			};
			var accCharCounter21 = new AccountChartCounter()
			{
				AccountType = "Purchases",
				AccountCategory = AccountCategoryEnum.StorePurchase,
				ParentAccNum = "4112000000",
				Count = 0,
				BalanceSheet = false,
				IncomeStatement = true
			};
			var accCharCounter22 = new AccountChartCounter()
			{
				AccountType = "Owners",
				AccountCategory = AccountCategoryEnum.OwnerEquity,
				ParentAccNum = "5110000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter23 = new AccountChartCounter()
			{
				AccountType = "OwnerWithdraw",
				AccountCategory = AccountCategoryEnum.OwnerEquity,
				ParentAccNum = "5120000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};
			var accCharCounter24 = new AccountChartCounter()
			{
				AccountType = "OtherCurrentLiabilties",
				AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
				ParentAccNum = "2260000000",
				Count = 0,
				BalanceSheet = true,
				IncomeStatement = false
			};

			//await _dbContext.AccountChartCounter.AddRangeAsync(
			//	accCharCounter1,
			//	accCharCounter2,
			//	accCharCounter3,
			//	accCharCounter4,
			//	accCharCounter5,
			//	accCharCounter6,
			//	accCharCounter7,
			//	accCharCounter8,
			//	accCharCounter9,
			//	accCharCounter10,
			//	accCharCounter11,
			//	accCharCounter12,
			//	accCharCounter13,
			//	accCharCounter14,
			//	accCharCounter15,
			//	accCharCounter16,
			//	accCharCounter17,
			//	accCharCounter18,
			//	accCharCounter19,
			//	accCharCounter20,
			//	accCharCounter21,
			//	accCharCounter22,
			//	accCharCounter23,
			//	accCharCounter24);

			await _dbContext.AccountChart.AddRangeAsync(
				new AccountChart()
				{
					AccountName = "Buildings",
					AccountNameAr = "مباني",
					AccNum = "1110000000",
					AccType = accCharCounter1,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Machines And Equipments",
					AccountNameAr = "أجهزة و معدات",
					AccNum = "1120000000",
					AccType = accCharCounter2,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Furnitiures",
					AccountNameAr = "أثاث",
					AccNum = "1130000000",
					AccType = accCharCounter3,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Safes",
					AccountNameAr = "خزن",
					AccNum = "1210000000",
					AccType = accCharCounter4,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Banks",
					AccountNameAr = "بنوك",
					AccNum = "1220000000",
					AccType = accCharCounter5,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Clients",
					AccountNameAr = "عملاء",
					AccNum = "1230000000",
					AccType = accCharCounter6,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Clients",
					AccountNameAr = "عملاء",
					AccNum = "1230000001",
					AccType = accCharCounter6,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "1230000000",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Checks",
					AccountNameAr = "شيكات",
					AccNum = "1240000000",
					AccType = accCharCounter7,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Checks In Safe",
					AccountNameAr = "شيكات في الخزنة",
					AccNum = "1240000001",
					AccType = accCharCounter7,
					IsParent = false,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "1240000000",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Checks In Bank",
					AccountNameAr = "شيكات في البنك",
					AccNum = "1240000002",
					AccType = accCharCounter7,
					IsParent = false,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "1240000000",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Bounced Checks",
					AccountNameAr = "شيكات مرتدة",
					AccNum = "1240000003",
					AccType = accCharCounter7,
					IsParent = false,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "1240000000",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Store",
					AccountNameAr = "مخزن",
					AccNum = "1250000000",
					AccType = accCharCounter8,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Custody",
					AccountNameAr = "عهد",
					AccNum = "1261000000",
					AccType = accCharCounter9,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "StaffAdvances",
					AccountNameAr = "سلف",
					AccNum = "1262000000",
					AccType = accCharCounter10,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Suppliers Advances",
					AccountNameAr = "دفعات مقدمة للموردين",
					AccNum = "1263000000",
					AccType = accCharCounter11,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "OtherCurrentAsset",
					AccountNameAr = "أصول متداولة أخري",
					AccNum = "1269000000",
					AccType = accCharCounter12,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "NotePayable",
					AccountNameAr = "شيكات موردين",
					AccNum = "2170000000",
					AccType = accCharCounter13,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "NotePayable",
					AccountNameAr = "شيكات موردين",
					AccNum = "2170000001",
					AccType = accCharCounter13,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "2170000000",
					IsActive = false,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Suppliers",
					AccountNameAr = "موردين",
					AccNum = "2210000000",
					AccType = accCharCounter14,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Taxes",
					AccountNameAr = "ضرائب",
					AccNum = "2220000000",
					AccType = accCharCounter15,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Creditors",
					AccountNameAr = "دائنون",
					AccNum = "2230000000",
					AccType = accCharCounter16,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Accrued Expenses",
					AccountNameAr = "مصروفات مستحقة",
					AccNum = "2240000000",
					AccType = accCharCounter17,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Advances Income",
					AccountNameAr = "ايرادات مقدمة",
					AccNum = "2250000000",
					AccType = accCharCounter18,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Advances Income",
					AccountNameAr = "ايرادات مقدمة",
					AccNum = "2250000001",
					AccType = accCharCounter18,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "2250000000",
					IsActive = false,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Other Current Liabilities",
					AccountNameAr = "التزمات متداولة أخري",
					AccNum = "2260000000",
					AccType = accCharCounter24,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Incomes",
					AccountNameAr = "إيرادات",
					AccNum = "3110000000",
					AccType = accCharCounter19,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "Expenses",
					AccountNameAr = "مصروفات",
					AccNum = "4111000000",
					AccType = accCharCounter20,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Purchases",
					AccountNameAr = "مشتريات",
					AccNum = "4112000000",
					AccType = accCharCounter21,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Debit
				},
				new AccountChart()
				{
					AccountName = "Capital",
					AccountNameAr = "رأس المال",
					AccNum = "5110000000",
					AccType = accCharCounter22,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				},
				new AccountChart()
				{
					AccountName = "WithDrawls",
					AccountNameAr = "مسحوبات",
					AccNum = "5120000000",
					AccType = accCharCounter23,
					IsParent = true,
					Currency = egp,
					Balance = 0m,
					Branch = mainBranch,
					ParentAcNum = "",
					IsActive = true,
					AccountNature = AccountNatureEnum.Credit
				});

			await _dbContext.SaveChangesAsync();
		}
	}
}
