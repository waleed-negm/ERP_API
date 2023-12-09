using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductTypesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductTypesController(ApplicationDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		// GET: Store/ProductTypes
		public async Task<IActionResult> Index()
		{
			return Ok(await _context.ProductTypes.ToListAsync());
		}

		[HttpGet]
		// GET: Store/ProductTypes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productType = await _context.ProductTypes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (productType == null)
			{
				return NotFound();
			}

			return Ok(productType);
		}

		// POST: Store/ProductTypes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,ProductTypeName")] ProductType productType)
		{
			if (ModelState.IsValid)
			{
				_context.Add(productType);
				await _context.SaveChangesAsync();
				//return RedirectToAction(nameof(Index));
			}
			return Ok(productType);
		}

		// POST: Store/ProductTypes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProductTypeName")] ProductType productType)
		{
			if (id != productType.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(productType);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductTypeExists(productType.Id))
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
			return Ok(productType);
		}

		// POST: Store/ProductTypes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var productType = await _context.ProductTypes.FindAsync(id);
			_context.ProductTypes.Remove(productType);
			await _context.SaveChangesAsync();
			//return RedirectToAction(nameof(Index));
			return Ok(productType);
		}

		private bool ProductTypeExists(long id)
		{
			return _context.ProductTypes.Any(e => e.Id == id);
		}
	}
}
