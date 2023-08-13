using Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.Services;
using Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
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
			_notesPayableManager.CollectNP(np);
			return Json
						(new { newLocation = "/Home/Index/" });
		}

		[HttpPost]
		public JsonResult MoveToCashCollection([FromBody] NPDetails np)
		{
			_notesPayableManager.MoveCheckToCashPayment(np);
			return Json
					   (new { newLocation = "/Home/Index/" });
		}

		[HttpPost]
		public JsonResult CollectCashCollection([FromBody] NPContainer vm)
		{
			_notesPayableManager.CollectCashNP(vm.SelectedNote, vm.PaymentDetails);
			return Json
					   (new { newLocation = "/Home/Index/" });
		}
	}
}
