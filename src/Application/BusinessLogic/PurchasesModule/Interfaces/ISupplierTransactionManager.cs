using Application.BusinessLogic.PurchasesModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel.Expenses;
using Domain.Entities;

namespace Application.BusinessLogic.PurchasesModule.Interfaces
{
	public interface ISupplierTransactionManager
	{
		void PurchaseSupplierTrans(PurchaseContainer vm, long purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter);
		void SupplierReturnTrans(Contacts Supplier, decimal LocalAmount, long CurrencyId, string TransId, decimal BalanceAfter);
		void ExpenseSupplierTrans(ExpenseVM vm, string SupplierAccNum, string TransId, decimal BalanceAfter);
		void SupplierPaymentTrans(SupplierPaymentContainer vm, decimal LocalAmount, string SupplierAccNum, string TransId, decimal BalanceAfter);
	}
}
