using Application.BusinessLogic.CurrentAssetModules.Inventory.ViewModel;
using Domain.Entities;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Interfaces
{
	public interface IStoreItemManager
	{
		void CreateStoreItem(StoreItemCreationVM store);
		Task<IEnumerable<StoreItem>> GetAllStoreItems();
		StoreItem GetStoreItemById(int Id);
	}
}