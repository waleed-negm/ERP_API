using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OpenBalanceController : Controller
	{
		private readonly IJournalManager _journalManager;
		private readonly IOpeningBalanceManager _openingBalanceManager;
		private readonly IAccountListManager _accountListManager;

		public OpenBalanceController(IOpeningBalanceManager openingBalanceManager,
			IJournalManager journalManager, IAccountListManager accountListManager)
		{
			_journalManager = journalManager;
			_openingBalanceManager = openingBalanceManager;
			_accountListManager = accountListManager;
		}

		[HttpGet]
		public IActionResult NewBalance()
		{
			var vm = _openingBalanceManager.NewOpeningTrans();
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult SaveOpeningBalance([FromBody] OpeningTransaction vm)
		{
			if (ModelState.IsValid)
			{
				_openingBalanceManager.SaveOpeningBalanceAsync(vm);
				return Json(new { newLocation = "/GLArea/AccountChart/Index" });
			}
			else
			{
				return Json(new { newLocation = "/home/index/" });
			}
		}

		[HttpPost]
		public IActionResult CreateJounral()
		{
			return Ok(new JournalVM());
		}

		[HttpPost]
		public JsonResult GetAccountDetails([FromBody] string Id)
		{
			if (!string.IsNullOrEmpty(Id))
			{
				var acc = _accountListManager.GetAccountDetails(Id);
				return Json(new { Account = acc });
			}
			return Json(new { error = "Please Choose Account" });
		}

		[HttpPost]
		public JsonResult SaveJournal([FromBody] JournalVM vm)
		{
			if (ModelState.IsValid)
			{
				_journalManager.SaveJournalAsync(vm);
				return Json(new { newLocation = "/GLArea/AccountChart/Index" });
			}
			else
			{
				var errors = ModelState.Values
							   .SelectMany(x => x.Errors)
							   .Select(x => x.ErrorMessage);


				return Json(new { errors });
			}
		}

	}
}
