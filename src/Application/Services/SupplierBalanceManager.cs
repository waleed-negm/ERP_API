using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
	public class SupplierBalanceManager : ISupplierBalanceManager
	{
		private readonly IApplicationDbContext _db;

		public SupplierBalanceManager(IApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<decimal> UpdateSupplierBalanceAsync(Contacts supplier, decimal LocalAmount, bool plus)
		{
			if (plus)
				supplier.SupplierBalance = supplier.SupplierBalance + LocalAmount;
			else
				supplier.SupplierBalance = supplier.SupplierBalance - LocalAmount;
			_db.Contacts.Update(supplier);
			await _db.SaveChangesAsync();
			return supplier.SupplierBalance;

		}

		public async Task ManageSupplierBalanceInCurrencyAsync(long SupplierId, string AccNum, long CurrencyId, decimal Amount, bool Plus)
		{
			var SupplierInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == SupplierId && x.AccNum == AccNum
															  && x.CurrencyId == CurrencyId).ToList();

			if (SupplierInCurrency.Count > 0)
				await UpdateBalanceInCurrencyAsync(SupplierId, AccNum, CurrencyId, Amount, Plus);
			else
				await AddNewBalanceInCurrencyAsync(SupplierId, AccNum, CurrencyId, Amount);

		}


		public async Task AddNewBalanceInCurrencyAsync(long SupplierId, string AccNum, long CurrencyId, decimal Amount)
		{
			_db.ContactBalanceInCurrency.Add(new ContactBalanceInCurrency()
			{
				AccNum = AccNum,
				ContactId = SupplierId,
				CurrencyId = CurrencyId,
				Balance = Amount
			});
			await _db.SaveChangesAsync();
		}


		public async Task UpdateBalanceInCurrencyAsync(long SupplierId, string AccNum, long CurrencyId, decimal Amount, bool Plus)
		{
			var SupplierInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == SupplierId && x.AccNum == AccNum
															  && x.CurrencyId == CurrencyId).FirstOrDefault();
			if (Plus)
				SupplierInCurrency.Balance = SupplierInCurrency.Balance + Amount;
			else
				SupplierInCurrency.Balance = SupplierInCurrency.Balance - Amount;


			_db.ContactBalanceInCurrency.Update(SupplierInCurrency);
			await _db.SaveChangesAsync();
		}


	}
}
