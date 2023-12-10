using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class NRController : Controller
	{
		private readonly NRManager _nRManager;

		public NRController(NRManager nRManager)
		{
			_nRManager = nRManager;
		}
		[HttpGet]
		public IActionResult CheckInSafe()
		{
			var vm = _nRManager.GetChecksInSafe();
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult MoveToBank([FromBody] CheckInSafeContainer vm)
		{
			if (ModelState.IsValid)
			{
				List<string> errors = new List<string>();
				try
				{
					_nRManager.MoveToBankAsync(vm);

					return Json(new
					{
						newLocation
						= "/SalesArea/NR/CheckInSafe"
					});
				}
				catch (Exception ex)
				{
					errors.Add(ex.Message);
					return Json(new { errors });
				}

			}
			else
			{
				var errors = ModelState.Values
								   .SelectMany(x => x.Errors)
								   .Select(x => x.ErrorMessage).ToList();

				return Json(new { errors });
			}
		}

		[HttpGet]
		public IActionResult CheckInBank()
		{
			var vm = _nRManager.GetCheckInBank();
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult CollectChecks([FromBody] CheckInBankContainer vm)
		{
			if (ModelState.IsValid)
			{
				List<string> errors = new List<string>();
				try
				{
					_nRManager.CollectCheckAsync(vm);

					return Json(new
					{
						newLocation
						= "/SalesArea/NR/CheckInBank/"
					});
				}
				catch (Exception ex)
				{
					errors.Add(ex.Message);
					return Json(new { errors });
				}

			}
			else
			{
				var errors = ModelState.Values
								   .SelectMany(x => x.Errors)
								   .Select(x => x.ErrorMessage).ToList();

				return Json(new { errors });
			}
		}
	}
}
