namespace Application.DTOs
{
	public class TaxBrackets
	{
		public decimal MinValue { get; set; }
		public decimal MaxValue { get; set; }
		public decimal TaxPrecentage { get; set; }
		public bool IsLastBraket { get; set; }
		public decimal MinRange { get; set; }
		public decimal MaxRange { get; set; }
		public int StartBrakcet { get; set; }
		public bool WithMaxValue { get; set; }
	}
}
