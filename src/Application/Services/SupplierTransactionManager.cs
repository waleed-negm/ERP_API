using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Extenstions;

namespace Application.Services
{
	public class SupplierTransactionManager : ISupplierTransactionManager
	{
		private readonly IApplicationDbContext _db;

		public SupplierTransactionManager(IApplicationDbContext db)
		{
			_db = db;
		}

		public async Task PurchaseSupplierTransAsync(PurchaseContainer vm, long purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter)
		{
			var trans = new SupplierTransaction();
			trans.SupplierId = vm.SupplierData.SupplierId;
			trans.PurchaseId = purchaseId;
			trans.TransId = TransId;
			trans.PaymentMenthod = SupplierPaymentMethod.Credit;
			trans.PaymentAccNum = SupplierAccNum;
			trans.PaymentDate = vm.PurchaseSummary.PurchaseDate.ToEgyptionDate();
			trans.CurrencyId = vm.PurchaseSummary.CurrencyId;
			trans.PaymentAmount = vm.PurchaseSummary.TotalAmountWithVAT;
			trans.BalanceAfter = BalanceAfter;
			_db.SupplierTransactions.Add(trans);
			await _db.SaveChangesAsync();
		}

		public async Task ExpenseSupplierTransAsync(ExpenseVM vm, string SupplierAccNum, string TransId, decimal BalanceAfter)
		{
			var trans = new SupplierTransaction();
			trans.SupplierId = vm.ExpenseDetails.SupplierId.Value;
			trans.TransId = TransId;
			trans.PaymentMenthod = SupplierPaymentMethod.Credit;
			trans.PaymentAccNum = SupplierAccNum;
			trans.PaymentDate = vm.ExpenseDetails.ExpenseDate.ToEgyptionDate();
			trans.CurrencyId = vm.ExpenseDetails.CurrencyId;
			trans.PaymentAmount = vm.ExpenseDetails.Amount - vm.PaymentDetails.PaymentAmount;
			trans.BalanceAfter = BalanceAfter;
			_db.SupplierTransactions.Add(trans);
			await _db.SaveChangesAsync();
		}

		public async Task SupplierPaymentTransAsync(SupplierPaymentContainer vm, decimal LocalAmount, string SupplierAccNum, string TransId, decimal BalanceAfter)
		{
			var trans = new SupplierTransaction();
			trans.SupplierId = vm.SupplierData.SupplierId;
			trans.TransId = TransId;
			trans.PaymentMenthod = vm.PaymentDetails.PaymentMethod;
			trans.PaymentAccNum = SupplierAccNum;
			trans.PaymentDate = vm.PaymentDetails.PaymentDate.ToEgyptionDate();
			trans.CurrencyId = vm.SelectedBalance.CurrencyId;
			trans.PaymentAmount = LocalAmount;
			trans.BalanceAfter = BalanceAfter;
			_db.SupplierTransactions.Add(trans);
			await _db.SaveChangesAsync();
		}

		public async Task SupplierReturnTransAsync(Contacts Supplier, decimal LocalAmount, long CurrencyId, string TransId, decimal BalanceAfter)
		{
			var trans = new SupplierTransaction();
			trans.SupplierId = Supplier.Id;
			trans.TransId = TransId;
			trans.PaymentMenthod = SupplierPaymentMethod.Credit;
			trans.PaymentAccNum = Supplier.SupplierAccNum;
			trans.PaymentDate = DateTime.Now;
			trans.CurrencyId = CurrencyId;
			trans.PaymentAmount = LocalAmount;
			trans.BalanceAfter = BalanceAfter;
			_db.SupplierTransactions.Add(trans);
			await _db.SaveChangesAsync();
		}
	}
}
