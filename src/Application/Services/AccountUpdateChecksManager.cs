using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Persistence;

namespace Application.Services
{
	public class AccountUpdateChecksManager : IAccountUpdateChecksManager
	{
		private readonly IApplicationDbContext _db;

		public AccountUpdateChecksManager(IApplicationDbContext db)
		{
			_db = db;
		}

		public bool ValidateCurrecny(string AccNum, int CurrecnyId)
		{
			var acc = _db.AccountChart.Find(AccNum);
			if (CurrecnyId != acc.CurrencyId && acc.Balance > 0)
				return false;
			else
				return true;
		}

		public bool ValidateBranch(string AccNum, int BranchId)
		{
			var acc = _db.AccountChart.Find(AccNum);
			if (BranchId != acc.BranchId && acc.Balance > 0)
				return false;
			else
				return true;
		}

		public IEnumerable<string> ValidateAccountData(UpdateAccountVM vm)
		{
			var errors = new List<string>();
			var acc = _db.AccountChart.Find(vm.AccNum);
			if (vm.CurrencyId != acc.CurrencyId && acc.Balance > 0)
				errors.Add("Balance Should be 0 to change currency");
			if (vm.BranchId != acc.BranchId && acc.Balance > 0)
				errors.Add("Balance Should be 0 to change branch");
			return errors;
		}
	}
}
