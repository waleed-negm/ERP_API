using Application.BusinessLogic.PurchasesModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel.Expenses;
using Application.BusinessLogic.PurchasesModule.ViewModel.ReturnBack;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.BusinessLogic.PurchasesModule.Interfaces
{
	public interface ISupplierJournalsManager
	{
		string PurchaseJournal(PurchaseContainer vm, Contacts Contact, IFormFile Invoice);
		string PurchaseReturnJournal(PurchaseReturnBackContainer vm, Contacts Contact, Currency Currency, bool IsVAT, decimal TotalInLocal, decimal TotalAmountWithVat);
		string ExpenseJounral(ExpenseVM vm, string ExpenseAccNum, Contacts Contact, Currency Currency);
		string SupplierPaymentJournal(SupplierPaymentContainer vm, Contacts Contact, decimal LocalAmount);
	}
}
