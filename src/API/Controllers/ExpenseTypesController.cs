using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ExpenseTypesController : Controller
	{
		private readonly IApplicationDbContext _context;

		public ExpenseTypesController(IApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return Ok(await _context.expenseTypes.ToListAsync());
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var expenseType = await _context.expenseTypes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (expenseType == null)
			{
				return NotFound();
			}

			return Ok(expenseType);
		}

		// POST: Expenditure/ExpenseTypes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,ExpenseTypeName")] ExpenseType expenseType)
		{
			if (ModelState.IsValid)
			{
				_context.expenseTypes.Add(expenseType);
				await _context.SaveChangesAsync();
				//return RedirectToAction(nameof(Index));
			}
			return Ok(expenseType);
		}

		// POST: Expenditure/ExpenseTypes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenseTypeName")] ExpenseType expenseType)
		{
			if (id != expenseType.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.expenseTypes.Update(expenseType);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ExpenseTypeExists(expenseType.Id))
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
			return Ok(expenseType);
		}

		[HttpPost]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var expenseType = await _context.expenseTypes.FindAsync(id);
			_context.expenseTypes.Remove(expenseType);
			await _context.SaveChangesAsync();
			//return RedirectToAction(nameof(Index));
			return Ok(expenseType);
		}

		private bool ExpenseTypeExists(long id)
		{
			return _context.expenseTypes.Any(e => e.Id == id);
		}
	}
}
