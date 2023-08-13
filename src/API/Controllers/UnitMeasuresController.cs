using Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Settings;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UnitMeasuresController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UnitMeasuresController(ApplicationDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		// GET: Store/UnitMeasures
		public async Task<IActionResult> Index()
		{
			return Ok(await _context.UnitMeasures.ToListAsync());
		}

		[HttpGet]
		// GET: Store/UnitMeasures/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var unitMeasure = await _context.UnitMeasures
				.FirstOrDefaultAsync(m => m.Id == id);
			if (unitMeasure == null)
			{
				return NotFound();
			}

			return Ok(unitMeasure);
		}

		// POST: Store/UnitMeasures/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,UnitName")] UnitMeasure unitMeasure)
		{
			if (ModelState.IsValid)
			{
				_context.Add(unitMeasure);
				await _context.SaveChangesAsync();
				//return RedirectToAction(nameof(Index));
			}
			return Ok(unitMeasure);
		}

		// POST: Store/UnitMeasures/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,UnitName")] UnitMeasure unitMeasure)
		{
			if (id != unitMeasure.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(unitMeasure);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UnitMeasureExists(unitMeasure.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				//return RedirectToAction(nameof(Index));
			}
			return Ok(unitMeasure);
		}

		// POST: Store/UnitMeasures/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var unitMeasure = await _context.UnitMeasures.FindAsync(id);
			_context.UnitMeasures.Remove(unitMeasure);
			await _context.SaveChangesAsync();
			//return RedirectToAction(nameof(Index));
			return Ok(unitMeasure);
		}

		private bool UnitMeasureExists(int id)
		{
			return _context.UnitMeasures.Any(e => e.Id == id);
		}
	}
}
