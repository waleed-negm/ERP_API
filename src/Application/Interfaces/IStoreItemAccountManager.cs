using Application.DTOs;

namespace Application.Interfaces
{
	public interface IStoreItemAccountManager
	{
		Task<StoreAccountsHelper> GenerateStoreItemAccountsAsync(StoreItemCreationVM vm);
	}
}
