namespace Application.DTOs
{
	public class StatmentConatiner
	{
		public string ReportURL { get; set; }
		public StatmentParams StatmentParams { get; set; } = new StatmentParams();
		public List<StatmentTransactions> Transactions { get; set; } = new List<StatmentTransactions>();
	}
}
