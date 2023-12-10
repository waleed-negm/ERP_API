using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SalaryController : Controller
	{
		private readonly SalaryBatchManager _salaryBatchManager;
		private readonly IApplicationDbContext _db;

		public SalaryController(SalaryBatchManager salaryBatchManager, IApplicationDbContext db)
		{
			_salaryBatchManager = salaryBatchManager;
			_db = db;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vm = _db.SalaryBatches.ToList();
			return Ok(vm);
		}

		[HttpGet]
		public IActionResult ManageBatch(int Id)
		{
			var vm = _salaryBatchManager.StartBatch(Id);
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult CalcTax([FromBody] BatchContainer vm)
		{
			_salaryBatchManager.CalcTax(vm.EmployeeSalaries);
			return Json
					   (new { result = vm });
		}

		[HttpPost]
		public JsonResult SaveBatch([FromBody] BatchContainer vm)
		{
			_salaryBatchManager.SaveSalaryAsync(vm);
			return Json(new { newLocation = "/Home/Index" });
		}
	}
}
