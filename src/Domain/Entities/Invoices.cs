using Application.BusinessLogic.CRM.Model;
using Application.BusinessLogic.ERPSettings.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.SalesModule.Model
{
	[Table("Finance_Sales_Invoices")]
	public class Invoices
	{
		[StringLength(6), Key]
		public string InoviceNum { get; set; }

		public int InvoiceCount { get; set; }

		public int ContactId { get; set; }
		public Contacts ContactDetails { get; set; }
		[Column(TypeName = "Date")]
		public DateTime InvoiceDate { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
		public int CurrencyId { get; set; }
		public Currency Currency { get; set; }

		public bool IsVAT { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal VATAmount { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalWithVAT { get; set; }

	}
}
