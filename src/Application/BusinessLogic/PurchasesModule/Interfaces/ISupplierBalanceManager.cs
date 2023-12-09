using Domain.Entities;

namespace Application.BusinessLogic.PurchasesModule.Interfaces
{
	public interface ISupplierBalanceManager
	{
		void AddNewBalanceInCurrency(long SupplierId, string AccNum, long CurrencyId, decimal Amount);
		void ManageSupplierBalanceInCurrency(long SupplierId, string AccNum, long CurrencyId, decimal Amount, bool Plus);
		void UpdateBalanceInCurrency(long SupplierId, string AccNum, long CurrencyId, decimal Amount, bool Plus);
		decimal UpdateSupplierBalance(Contacts supplier, decimal LocalAmount, bool plus);
	}
}
