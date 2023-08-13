namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel.AccountStatment
{
	public class StatmentTransactions
	{
		public string TransDate { get; set; }
		public decimal Debit { get; set; }
		public decimal Credit { get; set; }
		public string Description { get; set; }
		public decimal BalanceAfter { get; set; }
	}
}
