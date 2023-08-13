//using Luftborn.Platform.Application.Models.Results;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Extensions
//{
//	public static class HttpContextExtensions
//	{
//		public static BadRequestObjectResult HandleInvalidRequest(this ActionContext context)
//		{
//			var result = context.ModelState
//			.Select(x => new InvalidRequestResult(x.Key, x.Value.Errors.Select(x => x.ErrorMessage)));

//			return new BadRequestObjectResult(result);
//		}
//	}
//}
