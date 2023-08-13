using Application.BusinessLogic.CRM.Model;
using Application.BusinessLogic.PurchasesModule.ViewModel;

namespace Application.BusinessLogic.PurchasesModule.Interfaces
{
	public interface ISupplierTransactionManager
	{
		void PurchaseSupplierTrans(PurchaseContainer vm, int purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter);
		void SupplierReturnTrans(Contacts Supplier, decimal LocalAmount, int CurrencyId, string TransId, decimal BalanceAfter);
	}
}