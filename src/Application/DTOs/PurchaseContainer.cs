namespace Application.DTOs
{
	public class PurchaseContainer
	{
		public SupplierData SupplierData { get; set; } = new SupplierData();
		public PurchaseSummary PurchaseSummary { get; set; } = new PurchaseSummary();
		public List<PurchaseItemDetails> PurchaseDetails { get; set; } = new List<PurchaseItemDetails>();

		public bool IsWaitingAreaVisible { get; set; } // Upload data => load view
		public bool IsDetailAreaVisible { get; set; } = true; // Normal View
		public bool IsMessageAreaVisible { get; set; } // Show In case an error

		public string SaveURL { get; set; }
		public List<string> Messages { get; set; } = new List<string>();

	}
}
