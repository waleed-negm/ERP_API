using Application.BusinessLogic.CRM.Interfaces;
using Application.BusinessLogic.PurchasesModule.Interfaces;
using Application.BusinessLogic.PurchasesModule.Services;
using Application.Common.DTOs;
using Infrastructure.Persistence;
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

		public ApplicationDbContext _db { get; }

		public SupplierController(ISupplierGenerationManager supplierGenerationManager,
						IPurchaseManager purchaseManager, ApplicationDbContext db,
						SupplierPaymentsManager supplierPaymentsManager)
		{
			_supplierGenerationManager = supplierGenerationManager;
			_purchaseManager = purchaseManager;
			_supplierPaymentsManager = supplierPaymentsManager;
			_db = db;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ResponseDto result = await _supplierGenerationManager.GetAllAsync();
			if (result.Status) return Ok(result);
			else return BadRequest(result);
		}
		[HttpPost]
		public async Task<IActionResult> Create(SupplierDto model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			ResponseDto result = await _supplierGenerationManager.AddAsync(model);
			if (result.Status) return Ok(result);
			else return BadRequest(result);
		}

		//public IActionResult PurchaseIncovices()
		//{
		//    var vm = _db.Purchases.Include(x => x.SupplierDetails).Include(x => x.Currency).ToList();
		//    return Ok(vm);
		//}

		//public IActionResult ReturnBack(int Id)
		//{
		//    var vm = _purchaseManager.GetPurchaseData(Id);
		//    return Ok(vm);
		//}

		//public JsonResult ReturnPurchase([FromBody] PurchaseReturnBackContainer vm)
		//{

		//    return Json
		//               (new { newLocation = "/Home/Index/" });
		//}

		//[HttpPost]
		//[AutoValidateAntiforgeryToken]
		//public IActionResult Create(ContactCreatingViewModel supplier)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        _supplierGenerationManager.AddNewSupplier(supplier);
		//        return RedirectToAction(nameof(this.Index));
		//    }
		//    return Ok(supplier);
		//}


		//public IActionResult NewPurchase(int id)
		//{
		//    var vm = _purchaseManager.NewPurchase(id);
		//    vm.SaveURL = "/Expenditure/Supplier/SavePurchase";
		//    return Ok(vm);
		//}


		//public JsonResult GetItemBalance(int id)
		//{
		//    var currentqty = _db.StoreItems.Find(id).Qty;

		//    return Json(new { CurrentQty = currentqty });
		//}


		//public JsonResult SavePurchase([FromForm] IFormFile InvoiceFile)
		//{
		//    List<string> errors = new List<string>();
		//    var body = Request.Form["vm"];
		//    var model = JsonConvert.DeserializeObject<PurchaseContainer>(body);

		//    if (ModelState.IsValid)
		//    {
		//        try
		//        {
		//            _purchaseManager.SavePurchase(model, InvoiceFile);
		//            return Json
		//                (new { newLocation = "/Home/Index/" });
		//        }
		//        catch (Exception ex)
		//        {
		//            errors.Add(ex.Message);
		//            errors.Add("Please Contact System Admin");
		//            return Json(new { errors = errors });
		//        }

		//    }
		//    else
		//    {
		//        errors.AddRange(ModelState.Values
		//                           .SelectMany(x => x.Errors)
		//                           .Select(x => x.ErrorMessage));

		//        return Json(new { errors = errors });
		//    }
		//}

		//public IActionResult SupplierPayment(int id)
		//{
		//    var vm = _supplierPaymentsManager.NewPayment(id);
		//    return Ok(vm);
		//}

		////SaveSupplierPayment


		//public JsonResult SaveSupplierPayment([FromBody] SupplierPaymentContainer vm)
		//{
		//    List<string> errors = new List<string>();
		//    if (ModelState.IsValid)
		//    {
		//        try
		//        {
		//            _supplierPaymentsManager.SaveSupplierPayment(vm);
		//            return Json
		//                (new { newLocation = "/Home/Index/" });
		//        }
		//        catch (Exception ex)
		//        {
		//            errors.Add(ex.Message);
		//            errors.Add("Please Contact System Admin");
		//            return Json(new { errors = errors });
		//        }

		//    }
		//    else
		//    {
		//        errors.AddRange(ModelState.Values
		//                           .SelectMany(x => x.Errors)
		//                           .Select(x => x.ErrorMessage));

		//        return Json(new { errors = errors });
		//    }
		//}

	}
}
