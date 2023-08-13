using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ListController : Controller
	{
		private readonly ApplicationDbContext _db;

		public ListController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		public JsonResult GetSafeAccounts(int id)
		{
			var Safes = _db.AccountChart
			   .Where(x => x.AccTypeId == 4 && x.CurrencyId == id && x.IsParent == false)
			  .Select(x => new SelectListItem
			  {
				  Value = x.AccNum,
				  Text = x.AccountName + "( " + x.Balance + " )"
			  }).ToList();


			return new JsonResult(Safes);
		}

		[HttpGet]
		public JsonResult GetBankAccounts(int id)
		{
			var banks = _db.AccountChart
			   .Where(x => x.AccTypeId == 5 && x.CurrencyId == id && x.IsParent == false)
			   .Select(x => new SelectListItem
			   {
				   Value = x.AccNum,
				   Text = x.AccountName + "( " + x.Balance + " )"
			   }).ToList();

			return new JsonResult(banks);
		}
	}
}
