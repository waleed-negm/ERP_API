namespace Application.BusinessLogic.SalesModule.ViewModel.ClientStatment
{
	public class ClientStatmentContainer
	{
		public ClientStatmentContainer()
		{
			StatmentParams = new StatmentParams();
			Transactions = new List<StatmentTransactions>();
		}
		public string ReportURL { get; set; }
		public StatmentParams StatmentParams { get; set; }
		public List<StatmentTransactions> Transactions { get; set; }
	}
}
