using Application.DTOs;

namespace Application.Interfaces
{
	public interface IJournalManager
	{
		Task<string> SaveJournalAsync(JournalVM vm);
	}
}
