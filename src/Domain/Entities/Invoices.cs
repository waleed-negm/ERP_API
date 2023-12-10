using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Invoices : BaseModel
	{
		public long InoviceNum { get; set; }

		public long InvoiceCount { get; set; }

		public long ContactId { get; set; }

		public Contacts ContactDetails { get; set; }

		
		public DateTime InvoiceDate { get; set; }

		
		public decimal Amount { get; set; }

		public long CurrencyId { get; set; }

		public Currency Currency { get; set; }

		public bool IsVAT { get; set; }

		
		public decimal VATAmount { get; set; }

		
		public decimal TotalWithVAT { get; set; }

	}
}
