﻿using Domain.Enums;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel
{
	public class OpeningTransactionDetails
	{
		public string AccNum { get; set; }
		public string AccountName { get; set; }
		public TransactionSidesEnum Side { get; set; }
		public decimal Debit { get; set; }
		public decimal Credit { get; set; }
		public decimal UsedRate { get; set; }
		public string CurrecnyAbbr { get; set; }
		public int CurrencyId { get; set; }
	}
}
