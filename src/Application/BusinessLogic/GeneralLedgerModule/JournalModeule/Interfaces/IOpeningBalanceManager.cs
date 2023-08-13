using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Interfaces
{
	public interface IOpeningBalanceManager
	{
		OpeningTransaction NewOpeningTrans();
		void SaveOpeningBalance(OpeningTransaction vm);
	}
}