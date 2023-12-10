using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
	public class UploadManager : IUploadManager
	{
		//private readonly IWebHostEnvironment _env;

		//public UploadManager(IWebHostEnvironment env)
		//{
		//	_env = env;
		//}

		public string UploadedFile(IFormFile Pic, string FolderName)
		{
			string uniqueFileName = null;

			if (Pic != null)
			{
				var uploadsFolder = Path.Combine(/*_env.WebRootPath,*/ FolderName);
				uniqueFileName = Guid.NewGuid().ToString() + "_" + Pic.FileName;
				var filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					Pic.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}
	}
}
