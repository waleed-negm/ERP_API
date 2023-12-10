using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class ClientPayamentManager
	{
		private readonly IApplicationDbContext _db;
		private readonly ClientBalanceManager _clientBalanceManager;
		private readonly ClientTransactionManager _clientTransactionManager;
		private readonly ClientJournalManager _clientJournalManager;
		private readonly NRManager _NRManager;

		public ClientPayamentManager(IApplicationDbContext db, ClientBalanceManager clientBalanceManager, ClientTransactionManager clientTransactionManager, ClientJournalManager clientJournalManager, NRManager NRManager)
		{
			_db = db;
			_clientBalanceManager = clientBalanceManager;
			_clientTransactionManager = clientTransactionManager;
			_clientJournalManager = clientJournalManager;
			_NRManager = NRManager;
		}

		public ClientPaymentContainer NewPayment(int ClientId)
		{
			var vm = new ClientPaymentContainer();
			var client = _db.Contacts.Find(ClientId);
			vm.ClientData.ClientId = ClientId;
			vm.ClientData.ClientName = client.Name;
			vm.ClientData.Phone = client.Phone1;
			vm.ClientData.Balance = client.ClientBalance;

			vm.ClientBalanceDetails = _db.ContactBalanceInCurrency.Include(x => x.Currency)
										.Where(x => x.ContactId == ClientId
										&& x.AccNum == client.ClientAccNum)
										.Select(x => new ClientBalanceDetails()
										{
											Amount = x.Balance,
											CurrencyAbbr = x.Currency.CurrencyAbbrev,
											CurrencyId = x.CurrencyId,
											LocalAmount = x.Balance * x.Currency.Rate,
											Rate = x.Currency.Rate
										}).ToList();
			return vm;
		}

		public async Task SaveClientPaymentAsync(ClientPaymentContainer vm)
		{
			var Currecny = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
			var LocalAmount = vm.PaymentDetails.PaymentAmount * Currecny.Rate;
			var Client = _db.Contacts.Find(vm.ClientData.ClientId);
			// Update ClientBalance Contact
			var BalanceAfter = await _clientBalanceManager.UpdateClientBalanceAsync(Client, LocalAmount, false);
			// Update ClientBalance In Currency
			_clientBalanceManager.ManageClientBalanceInCurrency(Client.Id, Client.ClientAccNum, Currecny.Id, vm.PaymentDetails.PaymentAmount, false);
			// Journal
			var TransId = await _clientJournalManager.ClientPaymentJournalAsync(vm, Client, LocalAmount);
			// Add Client Transaction
			_clientTransactionManager.ClientPaymentTransactionAsync(vm, Client, TransId, BalanceAfter);
			// if Check => add new check
			if (vm.PaymentDetails.PaymentMethod == ClientPaymentMethod.Check)
			{
				await _NRManager.AddNewCheckAsync(vm, Currecny, TransId);
			}
			await _db.SaveChangesAsync();
		}
	}
}
