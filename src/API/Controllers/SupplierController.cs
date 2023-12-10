using Application.Common.DTOs;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : Controller
	{
		private readonly ISupplierGenerationManager _supplierGenerationManager;
		private readonly IPurchaseManager _purchaseManager;
		private readonly SupplierPaymentsManager _supplierPaymentsManager;
		//private readonly IApplicationDbContext _db;

		//public SupplierController(ISupplierGenerationManager supplierGenerationManager, IApplicationDbContext db, IPurchaseManager purchaseManager, SupplierPaymentsManager supplierPaymentsManager)
		public SupplierController(ISupplierGenerationManager supplierGenerationManager)
		{
			_supplierGenerationManager = supplierGenerationManager;
			//_purchaseManager = purchaseManager;
			//_supplierPaymentsManager = supplierPaymentsManager;
			//_db = db;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ResponseDto result = await _supplierGenerationManager.GetAllAsync();
			if (result.Status)
				return Ok(result);
			else
				return BadRequest(result);
		}
		[HttpPost]
		public async Task<IActionResult> Create(ContactCreatingViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			ResponseDto result = await _supplierGenerationManager.AddAsync(model);
			if (result.Status)
				return Ok(result);
			else
				return BadRequest(result);
		}

		//[HttpGet("PurchaseIncovices")]
		//public IActionResult PurchaseIncovices()
		//{
		//	var vm = _db.Purchases.Include(x => x.SupplierDetails).Include(x => x.Currency).ToList();
		//	return Ok(vm);
		//}

		//[HttpGet("ReturnBack/{id}")]
		//public IActionResult ReturnBack(int Id)
		//{
		//	var vm = _purchaseManager.GetPurchaseData(Id);
		//	return Ok(vm);
		//}

		//[HttpPost("ReturnPurchase")]
		//public IActionResult ReturnPurchase([FromBody] PurchaseReturnBackContainer vm)
		//{

		//	return Ok(new { newLocation = "/Home/Index/" });
		//}

		//[HttpGet("NewPurchase")]
		//public IActionResult NewPurchase(int id)
		//{
		//	var vm = _purchaseManager.NewPurchase(id);
		//	vm.SaveURL = "/Expenditure/Supplier/SavePurchase";
		//	return Ok(vm);
		//}

		//[HttpGet("GetItemBalance/{id}")]
		//public IActionResult GetItemBalance(int id)
		//{
		//	var currentqty = _db.StoreItems.Find(id).Qty;

		//	return Ok(new { CurrentQty = currentqty });
		//}

		//[HttpPost("SavePurchase")]
		//public IActionResult SavePurchase([FromForm] IFormFile InvoiceFile)
		//{
		//	List<string> errors = new List<string>();
		//	var body = Request.Form["vm"];
		//	var model = JsonConvert.DeserializeObject<PurchaseContainer>(body);

		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			_purchaseManager.SavePurchase(model, InvoiceFile);
		//			return Ok
		//				(new { newLocation = "/Home/Index/" });
		//		}
		//		catch (Exception ex)
		//		{
		//			errors.Add(ex.Message);
		//			errors.Add("Please Contact System Admin");
		//			return Ok(new { errors });
		//		}

		//	}
		//	else
		//	{
		//		errors.AddRange(ModelState.Values
		//						   .SelectMany(x => x.Errors)
		//						   .Select(x => x.ErrorMessage));

		//		return Ok(new { errors });
		//	}
		//}

		//[HttpGet("SupplierPayment/{id}")]
		//public IActionResult SupplierPayment(int id)
		//{
		//	var vm = _supplierPaymentsManager.NewPayment(id);
		//	return Ok(vm);
		//}

		////SaveSupplierPayment

		//[HttpPost("SaveSupplierPayment")]
		//public IActionResult SaveSupplierPayment([FromBody] SupplierPaymentContainer vm)
		//{
		//	List<string> errors = new List<string>();
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			_supplierPaymentsManager.SaveSupplierPayment(vm);
		//			return Ok
		//				(new { newLocation = "/Home/Index/" });
		//		}
		//		catch (Exception ex)
		//		{
		//			errors.Add(ex.Message);
		//			errors.Add("Please Contact System Admin");
		//			return Ok(new { errors });
		//		}

		//	}
		//	else
		//	{
		//		errors.AddRange(ModelState.Values
		//						   .SelectMany(x => x.Errors)
		//						   .Select(x => x.ErrorMessage));

		//		return Ok(new { errors });
		//	}
		//}

	}
}
