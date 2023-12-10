namespace Application.DTOs
{
	public class SalesContainer
	{
		public ClientData ClientData { get; set; } = new ClientData();
		public List<SalesItemDetails> SalesItemDetails { get; set; } = new List<SalesItemDetails>();
		public SalesSummary SalesSummary { get; set; } = new SalesSummary();
		public string SaveURL { get; set; }
	}
}
