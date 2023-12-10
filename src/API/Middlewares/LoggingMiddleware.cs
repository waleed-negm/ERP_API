namespace API.Middlewares
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LoggingMiddleware> _logger;

		public LoggingMiddleware(ILogger<LoggingMiddleware> logger, RequestDelegate next)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task Invoke(HttpContext context)
		{
			using (var logger = _logger.BeginScope("{IPAddress}/{Host}/{RequestTrace}", context.Connection.RemoteIpAddress, context.Request.Host.Host, context.TraceIdentifier))
			{
				await _next(context);
			}
		}
	}
}
