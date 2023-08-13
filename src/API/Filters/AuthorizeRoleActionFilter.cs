//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Text.Json;

//namespace API.Filters
//{
//	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//	public class AuthorizeRole : ActionFilterAttribute
//	{
//		private readonly string[] _roles;
//		public AuthorizeRole(params string[] roles)
//		{
//			_roles = roles ?? throw new ArgumentNullException(nameof(roles));
//		}

//		public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//		{
//			var currentClaims = context.HttpContext.User.Claims;
//			var extensionClaimsObject = currentClaims.FirstOrDefault(c => c.Type.Equals(CustomClaimsConstants.ExtensionClaims, StringComparison.InvariantCultureIgnoreCase));
//			var extensionClaims = extensionClaimsObject?.Value;
//			if (extensionClaims == null) return null;
//			var customClaims = JsonSerializer.Deserialize<CustomClaims>(extensionClaims);
//			var roles = customClaims.Roles;

//			var rolesAuthorized = roles.Any(r => _roles.ToList().Contains(r));

//			if (!rolesAuthorized)
//			{
//				throw new UnauthorizedAccessException("Unauthorized Role");
//			}

//			return base.OnActionExecutionAsync(context, next);
//		}
//	}
//}
