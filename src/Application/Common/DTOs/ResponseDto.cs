namespace Application.Common.DTOs
{
	public class ResponseDto
	{
		public bool Status { get; set; }
		public string message { get; set; }
		public object Body { get; set; }
		public int TotalCount { get; set; }
	}
}
