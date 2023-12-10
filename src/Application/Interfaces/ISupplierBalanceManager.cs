using Domain.Entities;

namespace Application.Interfaces
{
	public interface ISupplierBalanceManager
	{
		Task AddNewBalanceInCurrencyAsync(long SupplierId, string AccNum, long CurrencyId, decimal Amount);
		Task ManageSupplierBalanceInCurrencyAsync(long SupplierId, string AccNum, long CurrencyId, decimal Amount, bool Plus);
		Task UpdateBalanceInCurrencyAsync(long SupplierId, string AccNum, long CurrencyId, decimal Amount, bool Plus);
		Task<decimal> UpdateSupplierBalanceAsync(Contacts supplier, decimal LocalAmount, bool plus);
	}
}
