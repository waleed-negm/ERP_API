using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Interfaces
{
	public interface IJournalManager
	{
		JournalVM NewJournal();
		string SaveJournal(JournalVM vm);
	}
}