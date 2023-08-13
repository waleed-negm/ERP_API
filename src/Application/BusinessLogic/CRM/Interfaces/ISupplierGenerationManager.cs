using Application.Common.DTOs;

namespace Application.BusinessLogic.CRM.Interfaces
{
	public interface ISupplierGenerationManager
	{
		Task<ResponseDto> GetAllAsync();
		Task<ResponseDto> AddAsync(SupplierDto model);
	}
}