namespace Application.Configuration
{
	public class JWTConfiguration
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public double DurationInHours { get; set; }
	}
}
