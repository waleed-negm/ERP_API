using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces
{
	public interface IAccountGenerator
	{
		string GenerateAccount(CreateAccountVM newAccount);
		string CreateNewAccount(CreateAccountVM newAccount);
	}
}