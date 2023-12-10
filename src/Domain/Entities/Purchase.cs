using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Purchase : BaseModel
	{
		public long SupplierId { get; set; }

		[ForeignKey("SupplierId")]
		public Contacts SupplierDetails { get; set; }

		
		public DateTime PurchaseDate { get; set; }

		
		public decimal TotalAmount { get; set; }

		public bool IsVAT { get; set; }

		
		public decimal VATAmount { get; set; }

		
		public decimal TotalAmountWithVAT { get; set; }

		public bool IsFullyPaid { get; set; }

		
		public decimal Paid { get; set; }

		public string InvoiceNum { get; set; }

		public long CurrencyId { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }
	}
}
