using Application.BusinessLogic.CRM.Services;
using Application.BusinessLogic.CRM.ViewModel;
using Application.BusinessLogic.SalesModule.Service;
using Application.BusinessLogic.SalesModule.ViewModel;
using Application.BusinessLogic.SalesModule.ViewModel.ClientStatment;
using Application.BusinessLogic.SalesModule.ViewModel.Payment;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ClientsController : Controller
	{
		private readonly ClientGenerationManager _clientGenerationManager;
		private readonly SalesManager _salesManager;
		private readonly ClientPayamentManager _clientPayamentManager;
		private readonly ClientReports _clientReports;

		public ClientsController(ClientGenerationManager clientGenerationManager,
			SalesManager salesManager, ClientPayamentManager clientPayamentManager,
			ClientReports clientReports)
		{
			_clientGenerationManager = clientGenerationManager;
			_salesManager = salesManager;
			_clientPayamentManager = clientPayamentManager;
			_clientReports = clientReports;

		}

		[HttpGet]
		public IActionResult Index()
		{
			return Ok(_clientGenerationManager.GetAllClients());
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Create(ContactCreatingViewModel client)
		{
			if (ModelState.IsValid)
			{
				var feedback = _clientGenerationManager.AddNewClient(client);

				//if (feedback.Done)
				//return RedirectToAction(nameof(this.Index));
				//else
				{
					foreach (var item in feedback.Errors)
					{
						ModelState.AddModelError("error", item);
					}
					return Ok(client);
				}
			}
			return Ok(client);
		}

		[HttpPost]
		public IActionResult NewSale(int id)
		{
			var vm = _salesManager.NewSale(id);
			vm.SaveURL = "/SalesArea/Clients/SaveNewSale";
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult SaveNewSale([FromBody] SalesContainer vm)
		{
			List<string> errors = new List<string>();


			if (ModelState.IsValid)
			{
				try
				{
					_salesManager.SaveNewSale(vm);
					return Json
						(new { newLocation = "/Home/Index/" });
				}
				catch (Exception ex)
				{
					errors.Add(ex.Message);
					errors.Add("Please Contact System Admin");
					return Json(new { errors });
				}

			}
			else
			{
				errors.AddRange(ModelState.Values
								   .SelectMany(x => x.Errors)
								   .Select(x => x.ErrorMessage));

				return Json(new { errors });
			}
		}

		[HttpPost]
		public IActionResult ClientPayment(int id)
		{
			var vm = _clientPayamentManager.NewPayment(id);
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult SaveClientPayment([FromBody] ClientPaymentContainer vm)
		{

			_clientPayamentManager.SaveClientPayment(vm);
			return Json(new { newLocation = "/Home/Index/" });

		}

		[HttpGet]
		public IActionResult ClientStatment()
		{
			var vm = new ClientStatmentContainer();
			vm.ReportURL = "/SalesArea/Clients/BuildClientStatment";
			return Ok(vm);
		}

		[HttpPost]
		public JsonResult BuildClientStatment([FromBody] StatmentParams vm)
		{
			if (ModelState.IsValid)
			{
				var newStatment = new ClientStatmentContainer();
				newStatment.StatmentParams = vm;
				_clientReports.UpdateStatement(newStatment);
				return Json(new { result = newStatment });
			}
			else
			{
				return Json(new { newLocation = "/home/index/" });
			}
		}
	}
}
