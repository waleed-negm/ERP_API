using System.Security.Claims;
using System.Security.Principal;

namespace API.Extensions
{
	public static class IdentityExtensions
	{
		public static string GetEmail(this IPrincipal principal)
		{
			var claim = ((ClaimsIdentity)principal.Identity).FindFirst(ClaimTypes.Email);
			return claim != null ? claim.Value : string.Empty;
		}

		public static string GetId(this IPrincipal principal)
		{
			var claim = ((ClaimsIdentity)principal.Identity).FindFirst(ClaimTypes.NameIdentifier);
			return claim != null ? claim.Value : string.Empty;
		}
	}
}
