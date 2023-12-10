using AutoMapper;
using Infrastructure.Persistence.Extenstions;
using Domain.Entities;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
	public class SalesManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly ClientBalanceManager _clientBalanceManager;
		private readonly ClientJournalManager _clientJournalManager;
		private readonly ClientTransactionManager _clientTransactionManager;

		public SalesManager(IApplicationDbContext db, IMapper mapper,
				ClientBalanceManager clientBalanceManager,
				ClientJournalManager clientJournalManager,
				ClientTransactionManager clientTransactionManager)
		{
			_db = db;
			_mapper = mapper;
			_clientBalanceManager = clientBalanceManager;
			_clientJournalManager = clientJournalManager;
			_clientTransactionManager = clientTransactionManager;
		}

		public SalesContainer NewSale(int ClientId)
		{
			var vm = new SalesContainer();
			var client = _db.Contacts.Find(ClientId);
			vm.ClientData = new ClientData()
			{
				ClientId = ClientId,
				Balance = client.ClientBalance,
				ClientName = client.Name,
				Phone = client.Phone1
			};

			return vm;
		}

		public async Task SaveNewSaleAsync(SalesContainer vm)
		{
			var ClientData = _db.Contacts.Find(vm.ClientData.ClientId);
			var Currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
			var LocalAmount = vm.SalesSummary.TotalWithVAT * Currency.Rate;
			// Update Balance Contact
			var BalanceAfter = await _clientBalanceManager.UpdateClientBalanceAsync(ClientData, LocalAmount, true);
			// Update Balance In Currency
			_clientBalanceManager.ManageClientBalanceInCurrency(ClientData.Id, ClientData.ClientAccNum, Currency.Id, vm.SalesSummary.TotalWithVAT, true);
			// Create New Invoice
			var InvoiceNum = await NewInvoiceAsync(vm);
			// Income Journal  => Debit Client Credit Income
			var TransId = await _clientJournalManager.IncomeJournalAsync(vm, ClientData, null);
			// Purchase Jounral
			// Update StoreItemTable
			// Update StoreItemWithDetails
			// Store Transaction
			await _clientJournalManager.PurchaseJournalAsync(vm, InvoiceNum);
			// Client Transaction 
			await _clientTransactionManager.ClientSalesTransactionAsync(vm, ClientData, InvoiceNum, TransId);
			await _db.SaveChangesAsync();
		}

		private async Task<long> NewInvoiceAsync(SalesContainer vm)
		{
			var invoice = new Invoices();
			var inv = CreateNewInvoiceNum();
			invoice.InoviceNum = inv.InvNum;
			invoice.InvoiceCount = inv.LastId;
			invoice.ContactId = vm.ClientData.ClientId;
			invoice.CurrencyId = vm.SalesSummary.CurrencyId;
			invoice.Amount = vm.SalesSummary.TotalAmount;
			invoice.InvoiceDate = vm.SalesSummary.InvoiceDate.ToEgyptionDate();
			invoice.IsVAT = vm.SalesSummary.IsVAT;
			invoice.TotalWithVAT = vm.SalesSummary.TotalWithVAT;
			invoice.VATAmount = vm.SalesSummary.VATAmount;
			_db.Invoices.Add(invoice);
			await _db.SaveChangesAsync();
			return invoice.InoviceNum;
		}

		private InvoiceNum CreateNewInvoiceNum()
		{
			//_db.Invoices.Where(x=> x.InvoiceDate.Year == invoicedate.Year).Max(x=>x.InvoiceCount)

			var i = _db.Invoices.Count() == 0 ? 1 : _db.Invoices.Max(x => x.InvoiceCount) + 1;
			return new InvoiceNum()
			{
				InvNum = i,
				LastId = i
			};

		}

		private class InvoiceNum
		{
			public long InvNum { get; set; }
			public long LastId { get; set; }
		}

	}
}
