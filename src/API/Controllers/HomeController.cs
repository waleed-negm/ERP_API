using System.Diagnostics;
using Application.BusinessLogic.HR.DTOs;
using Application.BusinessLogic.HR.Services;
using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly TaxManager _taxManager;

		public HomeController(ILogger<HomeController> logger, TaxManager taxManager)
		{
			_logger = logger;
			_taxManager = taxManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vm = new TestTax();
			return Ok(vm);
		}

		[HttpPost]
		public IActionResult Index(TestTax vm)
		{
			ModelState.Clear();
			vm.YearlyTax = _taxManager.CalcTaxUpdated(vm.MonthlySalary, 9000);
			return Ok(vm);
		}
		[HttpGet]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
