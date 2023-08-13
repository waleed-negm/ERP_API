namespace Application.BusinessLogic.PurchasesModule.ViewModel.SupplierStatment
{
	public class StatmentParams
	{
		public int SupplierId { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public decimal StartBalance { get; set; }
		public decimal EndBalance { get; set; }
	}
}
