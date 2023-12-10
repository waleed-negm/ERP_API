using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
	public class ClientBalanceManager
	{
		private readonly IApplicationDbContext _db;

		public ClientBalanceManager(IApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<decimal> UpdateClientBalanceAsync(Contacts client, decimal LocalAmount, bool plus)
		{
			if (plus)
				client.ClientBalance = client.ClientBalance + LocalAmount;
			else
				client.ClientBalance = client.ClientBalance - LocalAmount;
			_db.Contacts.Update(client);
			await _db.SaveChangesAsync();
			return client.ClientBalance;

		}

		public void ManageClientBalanceInCurrency(long ClientId, string AccNum, long CurrencyId, decimal Amount, bool Plus)
		{
			var SupplierInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == ClientId && x.AccNum == AccNum
															  && x.CurrencyId == CurrencyId).ToList();

			if (SupplierInCurrency.Count > 0)
				UpdateBalanceInCurrencyAsync(ClientId, AccNum, CurrencyId, Amount, Plus);
			else
				AddNewBalanceInCurrencyAsync(ClientId, AccNum, CurrencyId, Amount);

		}

		public async Task AddNewBalanceInCurrencyAsync(long ClientId, string AccNum, long CurrencyId, decimal Amount)
		{
			_db.ContactBalanceInCurrency.Add(new ContactBalanceInCurrency()
			{
				AccNum = AccNum,
				ContactId = ClientId,
				CurrencyId = CurrencyId,
				Balance = Amount
			});
			await _db.SaveChangesAsync();
		}

		public async Task UpdateBalanceInCurrencyAsync(long ClientId, string AccNum, long CurrencyId, decimal Amount, bool Plus)
		{
			var ClientInCurrency = _db.ContactBalanceInCurrency.Where(x => x.ContactId == ClientId && x.AccNum == AccNum
															  && x.CurrencyId == CurrencyId).FirstOrDefault();
			if (Plus)
				ClientInCurrency.Balance = ClientInCurrency.Balance + Amount;
			else
				ClientInCurrency.Balance = ClientInCurrency.Balance - Amount;


			_db.ContactBalanceInCurrency.Update(ClientInCurrency);
			await _db.SaveChangesAsync();
		}



	}
}
