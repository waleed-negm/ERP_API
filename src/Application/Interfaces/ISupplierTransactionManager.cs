using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
	public interface ISupplierTransactionManager
	{
		Task PurchaseSupplierTransAsync(PurchaseContainer vm, long purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter);
		Task SupplierReturnTransAsync(Contacts Supplier, decimal LocalAmount, long CurrencyId, string TransId, decimal BalanceAfter);
		Task ExpenseSupplierTransAsync(ExpenseVM vm, string SupplierAccNum, string TransId, decimal BalanceAfter);
		Task SupplierPaymentTransAsync(SupplierPaymentContainer vm, decimal LocalAmount, string SupplierAccNum, string TransId, decimal BalanceAfter);
	}
}
