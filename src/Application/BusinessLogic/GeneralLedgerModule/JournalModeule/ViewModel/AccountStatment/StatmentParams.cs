﻿namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel.AccountStatment
{
	public class StatmentParams
	{
		public string AccNum { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public decimal StartBalance { get; set; }
		public decimal EndBalance { get; set; }
	}
}
