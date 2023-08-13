using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Services
{
	public class AccountListManager : IAccountListManager
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _map;

		public AccountListManager(ApplicationDbContext db, IMapper map)
		{
			_db = db;
			_map = map;
		}

		public AccountChart GetAccountDetails(string AccNum) =>
							_db.AccountChart.Include(x => x.Currency)
							.FirstOrDefault(x => x.AccNum == AccNum);


		public IEnumerable<AccountListVM> GetAllAccount() =>
												_map.Map<List<AccountListVM>>
												(_db.AccountChart.Include(x => x.AccType)
												.Include(x => x.Currency)
												.Include(x => x.Branch)
												//.Where(x => x.IsParent == false)
												.ToList());

		public UpdateAccountVM GetAccount(string AccNum) =>
												_map.Map<UpdateAccountVM>(_db.AccountChart.Find(AccNum));


		public void UpdateAccount(UpdateAccountVM account)
		{
			var acc = _db.AccountChart.Find(account.AccNum);
			acc.AccountName = account.AccountName;
			acc.AccountNameAr = account.AccountNameAr;
			acc.CurrencyId = account.CurrencyId;
			acc.BranchId = account.BranchId;
			acc.IsActive = account.IsActive;
			_db.AccountChart.Update(acc);
			_db.SaveChanges();
		}

	}
}