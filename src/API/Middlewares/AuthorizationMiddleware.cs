//namespace API.Middlewares
//{
//	public class AuthorizationMiddleware
//	{
//		private readonly ILogger<AuthorizationMiddleware> _logger;
//		private readonly IServiceScopeFactory _serviceScopeFactory;
//		private readonly RequestDelegate _next;

//		public AuthorizationMiddleware(ILogger<AuthorizationMiddleware> logger, IServiceScopeFactory serviceScopeFactory, RequestDelegate next)
//		{
//			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
//			_serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
//			_next = next ?? throw new ArgumentNullException(nameof(next));
//		}

//		public async Task Invoke(HttpContext context)
//		{
//			using var scope = _serviceScopeFactory.CreateScope();
//			var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
//			var userId = userService.UserId();

//			if (userId == null)
//			{
//				await _next(context);
//				return;
//			}

//			using var logger = _logger.BeginScope("{UserId}", userId);

//			var dbContext = scope.ServiceProvider.GetRequiredService<ILuftbornDbContext>();
//			var user = dbContext.Users.Find(userId);
//			if (user == null || user.DeletedAt != null)
//			{
//				throw new UnauthorizedAccessException(userId);
//			}
//			await _next(context);
//		}
//	}
//}
