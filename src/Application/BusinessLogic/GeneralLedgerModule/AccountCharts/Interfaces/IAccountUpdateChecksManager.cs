using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces
{
	public interface IAccountUpdateChecksManager
	{
		IEnumerable<string> ValidateAccountData(UpdateAccountVM vm);
		bool ValidateBranch(string AccNum, int BranchId);
		bool ValidateCurrecny(string AccNum, int CurrecnyId);
	}
}