using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Extenstions;

namespace Application.Services
{
	public class ClientTransactionManager
	{
		private readonly IApplicationDbContext _db;

		public ClientTransactionManager(IApplicationDbContext db)
		{
			_db = db;
		}

		public async Task ClientSalesTransactionAsync(SalesContainer vm, Contacts ClientData, long InvoiceNum, string TransId)
		{
			var CT = new ClientTransaction();
			CT.InvoiceNum = InvoiceNum;
			CT.PaymentAccNum = ClientData.ClientAccNum;
			CT.PaymentAmount = vm.SalesSummary.TotalWithVAT;
			CT.PaymentDate = vm.SalesSummary.InvoiceDate.ToEgyptionDate();
			CT.PaymentMenthod = ClientPaymentMethod.Credit;
			CT.TransId = TransId;
			CT.ClientId = vm.ClientData.ClientId;
			CT.CurrencyId = vm.SalesSummary.CurrencyId;
			CT.BalanceAfter = ClientData.ClientBalance;
			_db.ClientTransactions.Add(CT);
			await _db.SaveChangesAsync();
		}

		public async Task ClientPaymentTransactionAsync(ClientPaymentContainer vm, Contacts Client,
			string TransId, decimal BalanceAfter)
		{
			var CT = new ClientTransaction();
			CT.InvoiceNum = null;
			CT.PaymentAccNum = vm.PaymentDetails.PaymentMethod == ClientPaymentMethod.Safe ? vm.PaymentDetails.SafeAccNum
							 : vm.PaymentDetails.PaymentMethod == ClientPaymentMethod.Bank ? vm.PaymentDetails.BankAccNum
							 : "1240000001";
			CT.PaymentAmount = vm.PaymentDetails.PaymentAmount;
			CT.PaymentDate = vm.PaymentDetails.PaymentDate.ToEgyptionDate();
			CT.PaymentMenthod = vm.PaymentDetails.PaymentMethod;
			CT.TransId = TransId;
			CT.ClientId = vm.ClientData.ClientId;
			CT.CurrencyId = vm.SelectedBalance.CurrencyId;
			CT.BalanceAfter = BalanceAfter;
			_db.ClientTransactions.Add(CT);
			await _db.SaveChangesAsync();
		}
	}
}
