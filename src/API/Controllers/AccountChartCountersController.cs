using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountChartCountersController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AccountChartCountersController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: GLArea/AccountChartCounters
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return Ok(await _context.AccountChartCounter.ToListAsync());
		}

		// GET: GLArea/AccountChartCounters/Details/5
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var accountChartCounter = await _context.AccountChartCounter
				.FirstOrDefaultAsync(m => m.Id == id);
			if (accountChartCounter == null)
			{
				return NotFound();
			}

			return Ok(accountChartCounter);
		}

		// POST: GLArea/AccountChartCounters/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,AccountType,AccountCategory,ParentAccNum,Count,BalanceSheet,IncomeStatement")] AccountChartCounter accountChartCounter)
		{
			if (ModelState.IsValid)
			{
				_context.Add(accountChartCounter);
				await _context.SaveChangesAsync();
				//return RedirectToAction(nameof(Index));
			}
			return Ok(accountChartCounter);
		}

		// GET: GLArea/AccountChartCounters/Edit/5
		[HttpPut]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var accountChartCounter = await _context.AccountChartCounter.FindAsync(id);
			if (accountChartCounter == null)
			{
				return NotFound();
			}
			return Ok(accountChartCounter);
		}

		// POST: GLArea/AccountChartCounters/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,AccountType,AccountCategory,ParentAccNum,Count,BalanceSheet,IncomeStatement")] AccountChartCounter accountChartCounter)
		{
			if (id != accountChartCounter.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(accountChartCounter);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AccountChartCounterExists(accountChartCounter.Id))
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
			return Ok(accountChartCounter);
		}

		// POST: GLArea/AccountChartCounters/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var accountChartCounter = await _context.AccountChartCounter.FindAsync(id);
			_context.AccountChartCounter.Remove(accountChartCounter);
			await _context.SaveChangesAsync();
			//return RedirectToAction(nameof(Index));
			return Ok(accountChartCounter);
		}

		private bool AccountChartCounterExists(int id)
		{
			return _context.AccountChartCounter.Any(e => e.Id == id);
		}
	}
}