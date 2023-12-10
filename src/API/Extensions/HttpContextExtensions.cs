using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
	public static class HttpContextExtensions
	{
		public static BadRequestObjectResult HandleInvalidRequest(this ActionContext context)
		{
			IEnumerable<InvalidRequestResult> result = context.ModelState.Select(x => new InvalidRequestResult(x.Key, x.Value.Errors.Select(x => x.ErrorMessage)));
			return new BadRequestObjectResult(result);
		}
	}
}
