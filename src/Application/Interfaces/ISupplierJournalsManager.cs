using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
	public interface ISupplierJournalsManager
	{
		Task<string> PurchaseJournalAsync(PurchaseContainer vm, Contacts Contact, IFormFile Invoice);
		Task<string> PurchaseReturnJournalAsync(PurchaseReturnBackContainer vm, Contacts Contact, Currency Currency, bool IsVAT, decimal TotalInLocal, decimal TotalAmountWithVat);
		Task<string> ExpenseJounralAsync(ExpenseVM vm, string ExpenseAccNum, Contacts Contact, Currency Currency);
		Task<string> SupplierPaymentJournalAsync(SupplierPaymentContainer vm, Contacts Contact, decimal LocalAmount);
	}
}
