﻿namespace Application.DTOs
{
	public class PurchaseSummary
	{
		public decimal TotalAmount { get; set; }
		public bool IsVAT { get; set; }
		public decimal VATAmount { get; set; }
		public decimal TotalAmountWithVAT { get; set; }
		public string InvoiceNum { get; set; }
		public string PurchaseDate { get; set; }
		public string Description { get; set; }
		public int CurrencyId { get; set; }
		public decimal VATRate { get; set; } = .14M;
	}
}
