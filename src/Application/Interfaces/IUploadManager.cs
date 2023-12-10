using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
	public interface IUploadManager
	{
		string UploadedFile(IFormFile Pic, string FolderName);
	}
}