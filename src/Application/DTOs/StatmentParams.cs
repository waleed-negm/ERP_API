namespace Application.DTOs
{
	public class StatmentParams
	{
		public int ClientId { get; set; }
		public int SupplierId { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public decimal StartBalance { get; set; }
		public decimal EndBalance { get; set; }
		public string AccNum { get; set; }
	}
}
