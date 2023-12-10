namespace Application.DTOs
{
	public class ClientStatmentContainer
	{
		public string ReportURL { get; set; }
		public StatmentParams StatmentParams { get; set; } = new StatmentParams();
		public List<StatmentTransactions> Transactions { get; set; } = new List<StatmentTransactions>();
	}
}
