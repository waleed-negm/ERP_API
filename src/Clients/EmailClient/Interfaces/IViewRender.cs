using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Clients.EmailClient.Interfaces
{
	public interface IViewRender
	{
		public string Render<TModel>(string name, TModel model);
		public string Render<TModel>(string name, TModel model, ViewDataDictionary viewData);
	}
}
