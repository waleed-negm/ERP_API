using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
	public interface IAccountListManager
	{
		IEnumerable<AccountListVM> GetAllAccount();
		UpdateAccountVM GetAccount(string AccNum);
		Task UpdateAccountAsync(UpdateAccountVM account);
		AccountChart GetAccountDetails(string AccNum);
	}
}
