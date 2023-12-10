namespace Application.DTOs
{
	public class PurchaseStoreTransactions
	{
		public long StoreItemId { get; set; }
		public string StoreItemName { get; set; }
		public decimal QTY { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal ReturnedQTY { get; set; }
	}
}
