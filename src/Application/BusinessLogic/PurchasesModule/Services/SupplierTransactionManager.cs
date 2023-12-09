using Application.BusinessLogic.PurchasesModule.Interfaces;
using Application.BusinessLogic.PurchasesModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel.Expenses;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extenstions;

namespace Application.BusinessLogic.PurchasesModule.Services
{
	public class SupplierTransactionManager : ISupplierTransactionManager
	{
		private readonly ApplicationDbContext _db;

		public SupplierTransactionManager(ApplicationDbContext db)
		{
			_db = db;
		}

		public void PurchaseSupplierTrans(PurchaseContainer vm, long purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter)
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
			_db.SaveChanges();
		}

		public void ExpenseSupplierTrans(ExpenseVM vm, string SupplierAccNum, string TransId, decimal BalanceAfter)
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
			_db.SaveChanges();
		}

		public void SupplierPaymentTrans(SupplierPaymentContainer vm, decimal LocalAmount, string SupplierAccNum, string TransId, decimal BalanceAfter)
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
			_db.SaveChanges();
		}

		public void SupplierReturnTrans(Contacts Supplier, decimal LocalAmount, long CurrencyId, string TransId, decimal BalanceAfter)
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
			_db.SaveChanges();
		}
	}
}
