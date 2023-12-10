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
		public JsonResult CollectCheck([FromBody] NPDetails np)
		{
			_notesPayableManager.CollectNPAsync(np);
			return Json
						(new { newLocation = "/Home/Index/" });
		}

		[HttpPost]
		public JsonResult MoveToCashCollection([FromBody] NPDetails np)
		{
			_notesPayableManager.MoveCheckToCashPaymentAsync(np);
			return Json
					   (new { newLocation = "/Home/Index/" });
		}

		[HttpPost]
		public JsonResult CollectCashCollection([FromBody] NPContainer vm)
		{
			_notesPayableManager.CollectCashNPAsync(vm.SelectedNote, vm.PaymentDetails);
			return Json
					   (new { newLocation = "/Home/Index/" });
		}
	}
}
