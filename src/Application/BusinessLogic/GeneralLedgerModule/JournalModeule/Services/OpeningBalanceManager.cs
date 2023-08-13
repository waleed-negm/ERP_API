using Microsoft.EntityFrameworkCore;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Model;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel;
using Infrastructure.Persistence;
using Domain.Enums;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Services
{
	public class OpeningBalanceManager : IOpeningBalanceManager
	{
		private readonly ApplicationDbContext _db;

		public OpeningBalanceManager(ApplicationDbContext db)
		{
			_db = db;
		}

		public OpeningTransaction NewOpeningTrans()
		{
			var vm = new OpeningTransaction();
			vm.CurrentFinaicialPeriodId = _db.FiniacialPeriods
							.FirstOrDefault(x => x.IsActive).Id;

			vm.TransactionDetails = _db.AccountChart.Include(x => x.Currency).Where(x => x.IsParent == false)
				.Select(x => new OpeningTransactionDetails()
				{
					AccNum = x.AccNum,
					AccountName = x.AccountNameAr,
					Credit = 0,
					Debit = 0,
					Side = x.AccountNature == AccountNatureEnum.Debit ? TransactionSidesEnum.Debit : TransactionSidesEnum.Credit,
					CurrecnyAbbr = x.Currency.CurrencyAbbrev,
					CurrencyId = x.CurrencyId,
					UsedRate = x.Currency.Rate

				}).ToList();

			return vm;
		}

		public void SaveOpeningBalance(OpeningTransaction vm)
		{
			// 3 tables => jounral => jounral details & historicalBalance & UpdateBalances Account Chart

			// Journal
			// Journal Details
			// Update Balances & starting Balance in Account Chart
			// Historical Balance

			// jounral
			//mm-yyyy-count


			using (var transaction = _db.Database.BeginTransaction())
			{
				try
				{
					var jr = new Journal();
					var date = DateTime.UtcNow;
					var MaxCount = _db.Journal.Count() > 0 ? _db.Journal.Max(x => x.TransCount) + 1 : 1;
					jr.TransId = date.Month.ToString() +
									"-" + date.Year.ToString() +
									"-" + MaxCount
									.ToString();
					jr.EntryDate = date;
					jr.TransDesc = vm.TransDesc;
					jr.TransDate = DateTime.Parse(vm.TransDate);
					jr.SystemModule = vm.SystemModule;
					//jr.UserName = _httpContextAccessor.HttpContext.User.Identity.Name;
					_db.Journal.Add(jr);

					// Journal Details
					foreach (var item in vm.TransactionDetails)
					{
						var jrD = new JournalDetails();
						jrD.TransId = jr.TransId;
						jrD.AccNum = item.AccNum;
						jrD.CurrencyId = item.CurrencyId;
						jrD.UsedRate = item.UsedRate;
						jrD.Side = item.Side;
						var amount = item.Side == TransactionSidesEnum.Debit ? item.Debit : item.Credit;
						var amountLocal = amount * item.UsedRate;
						jrD.Amount = amount;
						jrD.AmountLocal = amountLocal;
						jrD.BalanceAfter = amount;
						_db.JournalDetails.Add(jrD);
						// Update Account Chart Balances With Same Currecny
						var account = _db.AccountChart.Find(item.AccNum);
						account.Balance = amount;
						account.StartingBalance = amount;
						_db.AccountChart.Update(account);
						// Update Parent Account With Amount Local (Sum All Child Account)
						var parentAccount = _db.AccountChart.Find(account.ParentAcNum);
						parentAccount.Balance = parentAccount.Balance + amountLocal;
						parentAccount.StartingBalance = parentAccount.StartingBalance + amountLocal;
						_db.AccountChart.Update(parentAccount);
						// Insert Historical Balance
						var HisBalance = new HistoricalBalance();
						HisBalance.AccNum = item.AccNum;
						HisBalance.Balance = amount;
						HisBalance.FinancialPeriodId = vm.CurrentFinaicialPeriodId;
						_db.HistoricalBalances.Add(HisBalance);
					}
					_db.SaveChanges();
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
				}
			}

		}
	}
}
