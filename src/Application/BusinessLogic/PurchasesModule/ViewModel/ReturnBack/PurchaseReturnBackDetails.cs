﻿namespace Application.BusinessLogic.PurchasesModule.ViewModel.ReturnBack
{
	public class PurchaseReturnBackDetails
	{
		public int SupplierId { get; set; }

		public string SupplierName { get; set; }

		public string PurchaseDate { get; set; }

		public decimal TotalAmount { get; set; }

		public decimal VATAmount { get; set; }

		public decimal TotalAmountWithVAT { get; set; }
		public string InvoiceNum { get; set; }
		public int CurrencyId { get; set; }
		public string CurrencyAbbr { get; set; }
	}
}