using Application.DTOs;

namespace Application.Interfaces
{
	public interface IOpeningBalanceManager
	{
		OpeningTransaction NewOpeningTrans();
		Task SaveOpeningBalanceAsync(OpeningTransaction vm);
	}
}
