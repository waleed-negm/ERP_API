using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
	public interface IStoreItemManager
	{
		Task CreateStoreItemAsync(StoreItemCreationVM store);
		Task<IEnumerable<StoreItem>> GetAllStoreItemsAsync();
		StoreItem GetStoreItemById(int Id);
	}
}
