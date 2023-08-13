namespace Application.BusinessLogic.PurchasesModule.ViewModel
{
	public class SupplierBalanceDetails
	{
		public int CurrencyId { get; set; }
		public decimal Amount { get; set; }
		public decimal LocalAmount { get; set; }
		public decimal Rate { get; set; }
		public string CurrencyAbbr { get; set; }
	}
}
