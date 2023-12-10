using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class SupplierReport
	{
		private readonly IApplicationDbContext _db;

		public SupplierReport(IApplicationDbContext db)
		{
			_db = db;
		}

		public decimal GetStartBalance(StatmentParams STParm, DateTime Start)
		{
			var transactions = _db.SupplierTransactions.Include(x => x.TransactionDetails)
								.Where(x => x.SupplierId == STParm.SupplierId
								&& x.PaymentDate < Start).OrderBy(x => x.Id).ToList();
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
			var statment = _db.SupplierTransactions.Include(x => x.TransactionDetails)
							.Where(x => x.SupplierId == STParm.SupplierId
							&& x.PaymentDate >= Start
							&& x.PaymentDate < End)
							.OrderBy(x => x.Id)
							.Select(x => new StatmentTransactions()
							{
								TransDate = x.PaymentDate.ToString("dd/MM/yyyy"),
								Description = x.TransactionDetails.TransDesc,
								Debit = x.PaymentMenthod != SupplierPaymentMethod.Credit ? x.PaymentAmount : 0,
								Credit = x.PaymentMenthod == SupplierPaymentMethod.Credit ? x.PaymentAmount : 0,
								BalanceAfter = x.BalanceAfter,
								PurchaseId = x.PurchaseId
							}).ToList();
			return statment;

		}
	}
}
