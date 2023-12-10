using Application.DTOs;

namespace Application.Interfaces
{
	public interface IAccountGenerator
	{
		Task<string> GenerateAccountAsync(CreateAccountVM newAccount);
		Task<string> CreateNewAccountAsync(CreateAccountVM newAccount);
	}
}
