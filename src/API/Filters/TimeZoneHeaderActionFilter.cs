//using Microsoft.AspNetCore.Mvc.Filters;

//namespace API.Filters
//{
//	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//	public class TimeZoneHeaderActionFilter : ActionFilterAttribute
//	{
//		public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//		{
//			var timeZoneHeader = context.HttpContext.Request.Headers[HttpConstants.TIME_ZONE_HEADER].ToString();

//			if (string.IsNullOrWhiteSpace(timeZoneHeader))
//			{
//				throw new BadHttpRequestException("Time zone header is missing");
//			}

//			return base.OnActionExecutionAsync(context, next);
//		}
//	}
//}
