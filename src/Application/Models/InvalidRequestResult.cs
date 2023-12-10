namespace Application.Models
{
	public class InvalidRequestResult
	{
		public string Field { get; set; }
		public IEnumerable<string> Errors { get; set; }

		public InvalidRequestResult(string filed, IEnumerable<string> errors)
		{
			Field = filed;
			Errors = errors;
		}
	}
}
