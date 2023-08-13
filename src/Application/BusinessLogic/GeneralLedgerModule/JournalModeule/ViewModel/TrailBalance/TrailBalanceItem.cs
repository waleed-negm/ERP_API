namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel.TrailBalance
{
	public class TrailBalanceItem
	{
		public string AccNum { get; set; }
		public string AccName { get; set; }
		public decimal StartingBalanceDebit { get; set; }
		public decimal StartingBalanceCredit { get; set; }
		public decimal TotalAmountDebit { get; set; }
		public decimal TotalAmountCredit { get; set; }

		public decimal EndingBalanceDebit { get; set; }
		public decimal EndingBalanceCredit { get; set; }
	}
}
