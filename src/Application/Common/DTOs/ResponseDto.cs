namespace Application.Common.DTOs
{
	public class ResponseBaseDto
	{
		public bool Status { get; set; }
		public string message { get; set; }
		public int TotalCount { get; set; }
	}
	public class ResponseDto<T> : ResponseBaseDto
	{
		public T Body { get; set; }
	}
	public class ResponseDto : ResponseBaseDto
	{
		public object Body { get; set; }
	}
}
