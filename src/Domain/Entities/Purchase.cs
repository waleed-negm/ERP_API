using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Purchase : BaseModel
	{
		public long SupplierId { get; set; }

		[ForeignKey("SupplierId")]
		public Contacts SupplierDetails { get; set; }

		[Column(TypeName = "Date")]
		public DateTime PurchaseDate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalAmount { get; set; }

		public bool IsVAT { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal VATAmount { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalAmountWithVAT { get; set; }

		public bool IsFullyPaid { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Paid { get; set; }

		public string InvoiceNum { get; set; }

		public long CurrencyId { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }
	}
}
