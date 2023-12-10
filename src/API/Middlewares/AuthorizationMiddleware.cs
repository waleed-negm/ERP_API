using Application.Interfaces;
using Infrastructure.Persistence;

namespace API.Middlewares
{
	public class AuthorizationMiddleware
	{
		private readonly ILogger<AuthorizationMiddleware> _logger;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly RequestDelegate _next;

		public AuthorizationMiddleware(ILogger<AuthorizationMiddleware> logger, IServiceScopeFactory serviceScopeFactory, RequestDelegate next)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
			_next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task Invoke(HttpContext context)
		{
			using var scope = _serviceScopeFactory.CreateScope();
			var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

			if (!userService.UserIdExist)
			{
				await _next(context);
				return;
			}

			var userId = userService.UserId;
			using var logger = _logger.BeginScope("{UserId}", userId);
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var user = dbContext.Users.Find(userId);
			if (user == null || user.IsDeleted)
			{
				throw new UnauthorizedAccessException();
			}
			await _next(context);
		}
	}
}
