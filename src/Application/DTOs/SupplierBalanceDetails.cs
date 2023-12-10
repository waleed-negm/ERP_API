namespace Application.DTOs
{
	public class SupplierBalanceDetails
	{
		public long CurrencyId { get; set; }
		public decimal Amount { get; set; }
		public decimal LocalAmount { get; set; }
		public decimal Rate { get; set; }
		public string CurrencyAbbr { get; set; }
	}
}
