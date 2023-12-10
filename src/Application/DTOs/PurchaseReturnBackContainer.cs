namespace Application.DTOs
{
	public class PurchaseReturnBackContainer
	{
		public int PurchaseId { get; set; }
		public PurchaseReturnBackDetails PurchaseDetails { get; set; } = new PurchaseReturnBackDetails();
		public List<PurchaseStoreTransactions> PurchaseStoreDetails { get; set; } = new List<PurchaseStoreTransactions>();
	}
}
