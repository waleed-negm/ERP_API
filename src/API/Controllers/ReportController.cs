//using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Services;
//using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel.AccountStatment;
//using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel.TrailBalance;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//	[Route("api/[controller]/[action]")]
//	[ApiController]
//	public class ReportController : Controller
//	{
//		private readonly StatmentManager _statmentManager;
//		private readonly TrailBalanceManager _trailBalanceManager;

//		public ReportController(StatmentManager statmentManager, TrailBalanceManager trailBalanceManager)
//		{
//			_statmentManager = statmentManager;
//			_trailBalanceManager = trailBalanceManager;
//		}

//		[HttpGet]
//		public IActionResult AccountStatment()
//		{
//			var vm = new StatmentConatiner();
//			vm.ReportURL = "/GLArea/Report/BuildStatment";
//			return Ok(vm);
//		}

//		[HttpPost]
//		public JsonResult BuildStatment([FromBody] StatmentParams vm)
//		{
//			if (ModelState.IsValid)
//			{
//				var newStatment = new StatmentConatiner();
//				newStatment.StatmentParams = vm;
//				_statmentManager.UpdateStatement(newStatment);
//				return Json(new { result = newStatment });
//			}
//			else
//			{
//				return Json(new { newLocation = "/home/index/" });
//			}
//		}

//		[HttpGet]
//		public IActionResult TrailBalance()
//		{
//			var vm = new TrailBalanceContainer();
//			vm.ReportURL = "/GLArea/Report/BuildTrail";
//			return Ok(vm);
//		}

//		[HttpPost]
//		public JsonResult BuildTrail([FromBody] TrailBalanceContainer vm)
//		{
//			if (ModelState.IsValid)
//			{
//				_trailBalanceManager.BuildTrailBalanceParent(vm);
//				return Json(new { result = vm });
//			}
//			else
//			{
//				return Json(new { newLocation = "/home/index/" });
//			}
//		}
//	}
//}
