using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ExpensesController : Controller
	{
		private readonly ExpensesManager _expensesManager;

		public ExpensesController(ExpensesManager expensesManager)
		{
			_expensesManager = expensesManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vm = _expensesManager.GetAllExpenseItems();
			return Ok(vm);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(ExpenseCreationVM vm)
		{
			await _expensesManager.AddNewExpenseItemAsync(vm);
			//return RedirectToAction(nameof(this.Index));
			return Ok();

		}

		[HttpGet]
		public IActionResult NewExpense()
		{
			var vm = new ExpenseVM();
			vm.SaveURL = "/Expenditure/Expenses/SaveExpenses";
			return Ok(vm);
		}

		[HttpPost]
		public async Task<IActionResult> SaveExpensesAsync([FromBody] ExpenseVM expense)
		{
			await _expensesManager.SaveNewExpenseAsync(expense);
			return Json
						(new { newLocation = "/Home/Index/" });
		}
	}
}
