namespace Application.BusinessLogic.PurchasesModule.ViewModel.ReturnBack
{
	public class PurchaseReturnBackContainer
	{
		public PurchaseReturnBackContainer()
		{
			PurchaseDetails = new PurchaseReturnBackDetails();
			PurchaseStoreDetails = new List<PurchaseStoreTransactions>();
		}
		public int PurchaseId { get; set; }
		public PurchaseReturnBackDetails PurchaseDetails { get; set; }
		public List<PurchaseStoreTransactions> PurchaseStoreDetails { get; set; }
	}
}
