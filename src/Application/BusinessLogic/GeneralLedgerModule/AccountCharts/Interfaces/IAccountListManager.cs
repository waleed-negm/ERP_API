using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces
{
	public interface IAccountListManager
	{
		IEnumerable<AccountListVM> GetAllAccount();
		UpdateAccountVM GetAccount(string AccNum);
		void UpdateAccount(UpdateAccountVM account);
		AccountChart GetAccountDetails(string AccNum);
	}
}