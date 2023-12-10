using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class StoreItemManager : IStoreItemManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly IStoreItemAccountManager _storeItemAccountManager;

		public StoreItemManager(IApplicationDbContext db, IMapper mapper,
			IStoreItemAccountManager storeItemAccountManager)
		{
			_db = db;
			_mapper = mapper;
			_storeItemAccountManager = storeItemAccountManager;
		}

		public async Task<IEnumerable<StoreItem>> GetAllStoreItemsAsync() => await
			_db.StoreItems.Include(x => x.Brand).Include(x => x.ProductType).Include(x => x.UnitMeasure)
			.ToListAsync();

		public StoreItem GetStoreItemById(int Id) =>
			 _db.StoreItems.Include(x => x.Brand).Include(x => x.ProductType).Include(x => x.UnitMeasure)
			.FirstOrDefault(x => x.Id == Id);

		public async Task CreateStoreItemAsync(StoreItemCreationVM store)
		{
			var StoreItem = _mapper.Map<StoreItem>(store);
			var Accounts = await _storeItemAccountManager.GenerateStoreItemAccountsAsync(store);
			StoreItem.StoreAccNum = Accounts.StoreAccNum;
			StoreItem.PurchaseAccNum = Accounts.PurchaseAccNum;
			_db.StoreItems.Add(StoreItem);
			await _db.SaveChangesAsync();
		}
	}
}
