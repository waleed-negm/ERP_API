using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
	public interface IPurchaseManager
	{
		PurchaseContainer NewPurchase(int SupplierId);
		Task SavePurchaseAsync(PurchaseContainer vm, IFormFile Invoicefile);
		PurchaseReturnBackContainer GetPurchaseData(int PurchaseId);
	}
}
