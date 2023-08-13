using Application.BusinessLogic.CRM.Model;
using Application.BusinessLogic.ERPSettings.Model;
using Application.BusinessLogic.PurchasesModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel.ReturnBack;
using Microsoft.AspNetCore.Http;

namespace Application.BusinessLogic.PurchasesModule.Interfaces
{
	public interface ISupplierJournalsManager
	{
		string PurchaseJournal(PurchaseContainer vm, Contacts Contact, IFormFile Invoice);
		string PurchaseReturnJournal(PurchaseReturnBackContainer vm, Contacts Contact, Currency Currency, bool IsVAT, decimal TotalInLocal, decimal TotalAmountWithVat);
	}
}