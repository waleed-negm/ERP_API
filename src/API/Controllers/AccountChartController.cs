using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountChartController : Controller
	{
		private readonly IAccountGenerator _accountGenerator;
		private readonly IAccountListManager _accountListManager;
		private readonly IAccountUpdateChecksManager _accountUpdateChecks;

		public AccountChartController(IAccountGenerator accountGenerator,
			IAccountListManager accountListManager, IAccountUpdateChecksManager accountUpdateChecks)
		{
			_accountGenerator = accountGenerator;
			_accountListManager = accountListManager;
			_accountUpdateChecks = accountUpdateChecks;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var vm = _accountListManager.GetAllAccount();
			return Ok(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateAccount(CreateAccountVM vm)
		{
			if (ModelState.IsValid)
			{
				_accountGenerator.GenerateAccount(vm);
				//return RedirectToAction(nameof(this.Index));
			}
			return Ok(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(UpdateAccountVM account)
		{
			if (ModelState.IsValid)
			{
				_accountListManager.UpdateAccount(account);
				//return RedirectToAction(nameof(this.Index));
			}
			return Ok(account);
		}

		[HttpGet]
		public IActionResult VerifyCurrecny(string accNum, int CurrencyId)
		{
			if (!_accountUpdateChecks.ValidateCurrecny(accNum, CurrencyId))
				return Json($"الرصيد أكير من صفر . لايمكن تعديل العملة");
			return Json(true);
		}

		[HttpGet]
		public IActionResult VerifyBranch(string accNum, int BranchId)
		{
			if (!_accountUpdateChecks.ValidateBranch(accNum, BranchId))
				return Json($"الرصيد أكير من صفر . لايمكن تعديل الفرع");
			return Json(true);
		}

	}
}
