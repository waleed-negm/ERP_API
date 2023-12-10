using System.Security.Claims;

namespace Application.Interfaces
{
	public interface IUserService
	{
		ClaimsPrincipal CurrentClaims { get; }
		Guid UserId { get; }
		bool UserIdExist { get; }
	}
}
