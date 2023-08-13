using Application.BusinessLogic.PurchasesModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel.ReturnBack;
using Microsoft.AspNetCore.Http;

namespace Application.BusinessLogic.PurchasesModule.Interfaces
{
	public interface IPurchaseManager
	{
		PurchaseContainer NewPurchase(int SupplierId);
		void SavePurchase(PurchaseContainer vm, IFormFile Invoicefile);
		PurchaseReturnBackContainer GetPurchaseData(int PurchaseId);
	}
}