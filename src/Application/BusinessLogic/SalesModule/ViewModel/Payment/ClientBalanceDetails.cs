﻿namespace Application.BusinessLogic.SalesModule.ViewModel.Payment
{
	public class ClientBalanceDetails
	{
		public int CurrencyId { get; set; }
		public decimal Amount { get; set; }
		public decimal LocalAmount { get; set; }
		public decimal Rate { get; set; }
		public string CurrencyAbbr { get; set; }
	}
}