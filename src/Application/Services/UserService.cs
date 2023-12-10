using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
	public class UserService : IUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		public ClaimsPrincipal CurrentClaims { get => _httpContextAccessor?.HttpContext?.User ?? throw new ArgumentNullException(nameof(_httpContextAccessor)); }

		public Guid UserId
		{
			get
			{
				var id = CurrentClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				return new(id ?? throw new ArgumentNullException(nameof(ClaimTypes.NameIdentifier)));
			}
		}

		public bool UserIdExist
		{
			get
			{
				var id = CurrentClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				return id is not null;
			}
		}
	}
}
