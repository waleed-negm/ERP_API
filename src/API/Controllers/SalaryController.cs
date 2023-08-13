using Application.BusinessLogic.HR.DTOs.Payroll;
using Application.BusinessLogic.HR.Services;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SalaryController : Controller
	{
		private readonly SalaryBatchManager _salaryBatchManager;
		private readonly ApplicationDbContext _db;

		public SalaryController(SalaryBatchManager salaryBatchManager, ApplicationDbContext db)
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
			_salaryBatchManager.SaveSalary(vm);
			return Json(new { newLocation = "/Home/Index" });
		}
	}
}
