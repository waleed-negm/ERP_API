using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StoreItemsController : Controller
	{
		private readonly IStoreItemManager _storeItemManager;

		public StoreItemsController(IStoreItemManager storeItemManager)
		{
			_storeItemManager = storeItemManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var StoreItems = await _storeItemManager.GetAllStoreItemsAsync();
			return Ok(StoreItems);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(StoreItemCreationVM vm)
		{
			if (ModelState.IsValid)
			{
				_storeItemManager.CreateStoreItemAsync(vm);
				//return RedirectToAction(nameof(this.Index));
			}
			return Ok(vm);
		}
	}
}
