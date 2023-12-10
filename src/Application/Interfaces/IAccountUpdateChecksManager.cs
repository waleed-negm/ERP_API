using Application.DTOs;

namespace Application.Interfaces
{
	public interface IAccountUpdateChecksManager
	{
		IEnumerable<string> ValidateAccountData(UpdateAccountVM vm);
		bool ValidateBranch(string AccNum, int BranchId);
		bool ValidateCurrecny(string AccNum, int CurrecnyId);
	}
}