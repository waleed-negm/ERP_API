namespace Application.BusinessLogic.PurchasesModule.ViewModel.SupplierStatment
{
	public class SupplierStatmentContainer
	{
		public SupplierStatmentContainer()
		{
			StatmentParams = new StatmentParams();
			Transactions = new List<StatmentTransactions>();
		}
		public string ReportURL { get; set; }
		public StatmentParams StatmentParams { get; set; }
		public List<StatmentTransactions> Transactions { get; set; }
	}
}
