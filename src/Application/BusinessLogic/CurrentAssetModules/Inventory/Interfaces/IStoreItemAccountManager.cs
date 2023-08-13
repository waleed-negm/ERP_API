using Application.BusinessLogic.CurrentAssetModules.Inventory.ViewModel;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Interfaces
{
	public interface IStoreItemAccountManager
	{
		StoreAccountsHelper GenerateStoreItemAccounts(StoreItemCreationVM vm);
	}
}