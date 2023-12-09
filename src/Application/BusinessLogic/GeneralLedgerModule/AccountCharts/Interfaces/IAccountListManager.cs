using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using Domain.Entities;

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