using Application.Common.DTOs;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface ISupplierGenerationManager
	{
		Task<ResponseDto> GetAllAsync();
		Task<ResponseDto> AddAsync(ContactCreatingViewModel model);
	}
}
