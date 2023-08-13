namespace Application.Models
{
	public class FeedBack
	{
		public FeedBack()
		{
			Errors = new List<string>();
		}
		public bool Done { get; set; }
		public List<string> Errors { get; set; }
	}
}
