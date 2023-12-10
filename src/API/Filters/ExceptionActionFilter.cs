using System.Net;
using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
	public class ExceptionActionFilter : ExceptionFilterAttribute
	{
		private readonly ILogger<ExceptionActionFilter> _logger;

		public ExceptionActionFilter(ILogger<ExceptionActionFilter> logger)
		{
			_logger = logger;
		}

		public override void OnException(ExceptionContext context)
		{
			ResponseDto errorResponse = new();
			var httpResponseCode = GetStatusCode(context.Exception);
			if (context.HttpContext.Response.StatusCode == 403)
			{
				errorResponse.message = "Not Authorized";
			}
			else
			{
				errorResponse.message = context.Exception.Message;
				_logger.LogError(context.Exception, "An unhandled exception occurred.");
			}
			context.Result = new ObjectResult(errorResponse)
			{
				StatusCode = httpResponseCode
			};
			context.ExceptionHandled = true;
			base.OnException(context);
		}

		private static int GetStatusCode(Exception exception) => exception switch
		{
			ArgumentException _ or ArgumentNullException _ or FormatException _ => (int)HttpStatusCode.BadRequest,
			NotImplementedException _ => (int)HttpStatusCode.NotImplemented,
			AccessViolationException _ or UnauthorizedAccessException _ => (int)HttpStatusCode.Unauthorized,
			_ => (int)HttpStatusCode.InternalServerError,
		};

	}
}
