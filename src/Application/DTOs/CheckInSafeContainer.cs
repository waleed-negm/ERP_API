namespace Application.DTOs
{
	public class CheckInSafeContainer
	{
		public HafzaDetails HafzaDetails { get; set; } = new HafzaDetails();
		public List<CheckInSafeDetails> CheckDetails { get; set; } = new List<CheckInSafeDetails>();
	}
}
