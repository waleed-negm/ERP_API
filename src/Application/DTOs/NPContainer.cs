namespace Application.DTOs
{
	public class NPContainer
	{
		public List<NPDetails> CheckUnderCollection { get; set; } = new List<NPDetails>();
		public List<NPDetails> CheckCashCollection { get; set; } = new List<NPDetails>();
		public NPDetails SelectedNote { get; set; } = new NPDetails();
		public PaymentDetails PaymentDetails { get; set; } = new PaymentDetails();
	}
}
