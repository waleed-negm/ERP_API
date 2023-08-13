namespace Application.BusinessLogic.SalesModule.ViewModel.ClientStatment
{
	public class StatmentParams
	{
		public int ClientId { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public decimal StartBalance { get; set; }
		public decimal EndBalance { get; set; }
	}
}
