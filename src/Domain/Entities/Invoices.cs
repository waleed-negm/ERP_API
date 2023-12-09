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

		[Column(TypeName = "Date")]
		public DateTime InvoiceDate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }

		public long CurrencyId { get; set; }

		public Currency Currency { get; set; }

		public bool IsVAT { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal VATAmount { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalWithVAT { get; set; }

	}
}
