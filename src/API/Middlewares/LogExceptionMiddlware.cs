namespace API.Middlewares
{
	public class LogExceptionMiddlware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LogExceptionMiddlware> _logger;

		public LogExceptionMiddlware(ILogger<LogExceptionMiddlware> logger, RequestDelegate next)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, "Exception {StackTrace}/{@innerException}", exception.StackTrace, exception.InnerException);
			}
		}
	}
}
