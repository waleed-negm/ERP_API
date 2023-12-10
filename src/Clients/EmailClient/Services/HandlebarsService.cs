using Clients.EmailClient.Interfaces;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Clients.EmailClient.Services
{
	public class HandlebarsService : IViewRender
	{
		public string Render<TModel>(string name, TModel model)
		{
			return Render(name, model, null);
		}

		public string Render<TModel>(string name, TModel model, ViewDataDictionary viewData)
		{
			return BuildTemplate(name, model);
		}

		private string BuildTemplate(string source, object model)
		{
			var template = Handlebars.Compile(source);
			var result = template(model);
			return result;
		}
	}
}
