using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Extenstions;
using Domain.Enums;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
	public class StatmentManager
	{
		private readonly IApplicationDbContext _db;

		public StatmentManager(IApplicationDbContext db)
		{
			_db = db;
		}

		public void UpdateStatement(StatmentConatiner vm)
		{
			var Start = vm.StatmentParams.StartDate.ToEgyptionDate();
			var End = vm.StatmentParams.EndDate.ToEgyptionDate().AddDays(1);

			vm.Transactions = GetTransactions(vm.StatmentParams, Start, End);
			vm.StatmentParams.StartBalance = GetStartBalance(vm.StatmentParams, Start);
			if (vm.Transactions.Count > 0)
			{
				vm.StatmentParams.EndBalance = vm.Transactions.Last().BalanceAfter;
			}
			else
			{
				vm.StatmentParams.EndBalance = 0;
			}

		}


		public decimal GetStartBalance(StatmentParams STParm, DateTime Start)
		{
			var transactions = _db.JournalDetails.Include(x => x.Trans)
								.Where(x => x.AccNum == STParm.AccNum
								&& x.Trans.TransDate < Start).OrderBy(x => x.Trans.EntryDate).ToList();
			if (transactions.Count > 0)
			{
				return transactions.Last().BalanceAfter;
			}
			else
			{
				return 0;
			}

		}

		public List<StatmentTransactions> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End)
		{


			var statment = _db.JournalDetails.Include(x => x.Trans)
							.Where(x => x.AccNum == STParm.AccNum
							&& x.Trans.TransDate >= Start
							&& x.Trans.TransDate < End)
							.OrderBy(x => x.Trans.EntryDate)
							.Select(x => new StatmentTransactions()
							{
								TransDate = x.Trans.TransDate.ToString("dd/MM/yyyy"),
								Description = x.Trans.TransDesc,
								Debit = x.Side == TransactionSidesEnum.Debit ? x.Amount : 0,
								Credit = x.Side == TransactionSidesEnum.Credit ? x.Amount : 0,
								BalanceAfter = x.BalanceAfter
							}).ToList();

			return statment;

			//02/12/2020 00:00:00
		}
	}
}
