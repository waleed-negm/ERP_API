//using System.Text;
//using Newtonsoft.Json.Linq;

//namespace API.Middlewares
//{
//	public class ClientTimeZoneMiddleware
//	{
//		private readonly ILogger<ClientTimeZoneMiddleware> _logger;
//		private readonly RequestDelegate _next;
//		public ClientTimeZoneMiddleware(
//			RequestDelegate next,
//			ILogger<ClientTimeZoneMiddleware> logger
//		)
//		{
//			_next = next ?? throw new ArgumentNullException(nameof(next));
//			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
//		}

//		public async Task Invoke(HttpContext context)
//		{
//			if ((context.Request.Method == "POST" || context.Request.Method == "PUT") && context.Request.ContentType != null && context.Request.ContentType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
//			{
//				using (var reader = new StreamReader(context.Request.Body))
//				{
//					var body = await reader.ReadToEndAsync();

//					if (string.IsNullOrWhiteSpace(body))
//					{
//						await _next(context);
//						return;
//					}

//					var jsonObject = JObject.Parse(body);

//					var properties = jsonObject.Descendants()
//						.Where(p => p.Type == JTokenType.Date)
//						.ToList();

//					foreach (var property in properties)
//					{
//						var date = (DateTime)property;
//						property.Replace(new JValue(SetDateTimeKindToUtc(date)));
//					}

//					var buffer = Encoding.UTF8.GetBytes(jsonObject.ToString());
//					context.Request.Body = new MemoryStream(buffer);
//					context.Request.ContentLength = buffer.Length;
//				}
//			}

//			await _next(context);
//		}

//		private DateTime SetDateTimeKindToUtc(DateTime? value)
//		{
//			var utcDateTime = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
//			return utcDateTime;
//		}
//	}
//}
