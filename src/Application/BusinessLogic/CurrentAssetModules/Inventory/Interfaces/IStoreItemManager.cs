using Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Main;
using Application.BusinessLogic.CurrentAssetModules.Inventory.ViewModel;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Interfaces
{
	public interface IStoreItemManager
	{
		void CreateStoreItem(StoreItemCreationVM store);
		Task<IEnumerable<StoreItem>> GetAllStoreItems();
		StoreItem GetStoreItemById(int Id);
	}
}