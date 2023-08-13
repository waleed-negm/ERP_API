using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Model;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel;
using Domain.Enums;
using Infrastructure.Persistence;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Services
{
	public class JournalManager : IJournalManager
	{
		private readonly ApplicationDbContext _db;

		public JournalManager(ApplicationDbContext db)
		{
			_db = db;
		}

		public JournalVM NewJournal()
		{
			return new JournalVM();
		}

		public string SaveJournal(JournalVM vm)
		{
			var TransId = string.Empty;

			// Create Jounral

			var jr = new Journal();
			var date = DateTimeOffset.Now;
			var MaxCount = _db.Journal.Count() > 0 ? _db.Journal.Max(x => x.TransCount) + 1 : 1;

			jr.TransId = date.Month.ToString() +
							"-" + date.Year.ToString() +
							"-" + MaxCount
							.ToString();
			jr.EntryDate = date;
			jr.TransDesc = vm.TransDesc;
			jr.DocName = vm.DocName;
			jr.TransDate = DateTime.Parse(vm.TransDate);
			jr.SystemModule = vm.SystemModule;
			//jr.UserName = _httpContextAccessor.HttpContext.User.Identity.Name;
			_db.Journal.Add(jr);

			// Jouranl Details => update Account Chart Balance
			foreach (var item in vm.TransactionDetails)
			{
				var account = _db.AccountChart.Find(item.AccNum);
				var ParentAccount = _db.AccountChart.Find(account.ParentAcNum);
				var amount = item.Side == TransactionSidesEnum.Debit ? item.Debit : item.Credit;
				var amountLocal = amount * item.UsedRate;
				var jrD = new JournalDetails();
				jrD.TransId = jr.TransId;
				jrD.AccNum = item.AccNum;
				jrD.Amount = amount;
				jrD.AmountLocal = amountLocal;
				jrD.CurrencyId = item.CurrencyId;
				jrD.UsedRate = item.UsedRate;
				jrD.Side = item.Side;
				if (account.AccountNature == AccountNatureEnum.Debit)
				{
					if (item.Side == TransactionSidesEnum.Debit)
					{
						account.Balance = account.Balance + amount;
						ParentAccount.Balance = ParentAccount.Balance + amountLocal;
						jrD.BalanceAfter = account.Balance;
					}
					if (item.Side == TransactionSidesEnum.Credit)
					{
						account.Balance = account.Balance - amount;
						ParentAccount.Balance = ParentAccount.Balance - amountLocal;
						jrD.BalanceAfter = account.Balance;
					}
				}
				if (account.AccountNature == AccountNatureEnum.Credit)
				{
					if (item.Side == TransactionSidesEnum.Debit)
					{
						account.Balance = account.Balance - amount;
						ParentAccount.Balance = ParentAccount.Balance - amountLocal;
						jrD.BalanceAfter = account.Balance;
					}
					if (item.Side == TransactionSidesEnum.Credit)
					{
						account.Balance = account.Balance + amount;
						ParentAccount.Balance = ParentAccount.Balance + amountLocal;
						jrD.BalanceAfter = account.Balance;
					}
				}
				_db.AccountChart.Update(account);
				_db.AccountChart.Update(ParentAccount);
				_db.JournalDetails.Add(jrD);

			}
			_db.SaveChanges();

			TransId = jr.TransId;
			return TransId;
		}
	}
}
