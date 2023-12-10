using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BrandsController : Controller
	{
		private readonly IApplicationDbContext _context;

		public BrandsController(IApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Store/Brands
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return Ok(await _context.Brands.ToListAsync());
		}

		// GET: Store/Brands/Details/5
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var brand = await _context.Brands
				.FirstOrDefaultAsync(m => m.Id == id);
			if (brand == null)
			{
				return NotFound();
			}

			return Ok(brand);
		}

		// POST: Store/Brands/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,BrandName")] Brand brand)
		{
			if (ModelState.IsValid)
			{
				_context.Brands.Add(brand);
				await _context.SaveChangesAsync();
				//return RedirectToAction(nameof(Index));
			}
			return Ok(brand);
		}

		// POST: Store/Brands/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName")] Brand brand)
		{
			if (id != brand.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Brands.Update(brand);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BrandExists(brand.Id))
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
			return Ok(brand);
		}

		// POST: Store/Brands/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var brand = await _context.Brands.FindAsync(id);
			_context.Brands.Remove(brand);
			await _context.SaveChangesAsync();
			//return RedirectToAction(nameof(Index));
			return Ok(brand);
		}

		private bool BrandExists(long id)
		{
			return _context.Brands.Any(e => e.Id == id);
		}
	}
}
