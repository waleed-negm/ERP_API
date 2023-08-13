using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
	public interface IUploadManager
	{
		string UploadedFile(IFormFile Pic, string FolderName);
	}
}