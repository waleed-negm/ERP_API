using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class NPController : Controller
	{
		private readonly NotesPayableManager _notesPayableManager;

		public NPController(NotesPayableManager notesPayableManager)
		{
			_notesPayableManager = notesPayableManager;
		}

		[HttpGet]
		public IActionResult ManageNP()
		{
			var vm = _notesPayableManager.GetAllNP();
			return Ok(vm);
		}

		[HttpPost]
		public async Task<JsonResult> CollectCheckAsync([FromBody] NPDetails np)
		{
			await _notesPayableManager.CollectNPAsync(np);
			return Json
						(new { newLocation = "/Home/Index/" });
		}

		[HttpPost]
		public async Task<JsonResult> MoveToCashCollectionAsync([FromBody] NPDetails np)
		{
			await _notesPayableManager.MoveCheckToCashPaymentAsync(np);
			return Json
					   (new { newLocation = "/Home/Index/" });
		}

		[HttpPost]
		public async Task<JsonResult> CollectCashCollectionAsync([FromBody] NPContainer vm)
		{
			await _notesPayableManager.CollectCashNPAsync(vm.SelectedNote, vm.PaymentDetails);
			return Json
					   (new { newLocation = "/Home/Index/" });
		}
	}
}
