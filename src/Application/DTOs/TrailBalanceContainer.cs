namespace Application.DTOs
{
	public class TrailBalanceContainer
	{
		public TrailBalanceParams TrailBalanceParams { get; set; } = new TrailBalanceParams();
		public List<TrailBalanceItem> TrailBalanceItems { get; set; } = new List<TrailBalanceItem>();
		public decimal TotalStartingBalanceDebit { get; set; }
		public decimal TotalStartingBalanceCredit { get; set; }
		public decimal GrandTotalAmountDebit { get; set; }
		public decimal GrandTotalAmountCredit { get; set; }
		public decimal TotalEndingBalanceDebit { get; set; }
		public decimal TotalEndingBalanceCredit { get; set; }
		public string ReportURL { get; set; }
	}
}
