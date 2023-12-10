using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class StoreTransaction : BaseModel
	{
		public long StoreItemId { get; set; }

		[ForeignKey("StoreItemId")]
		public StoreItem StoreItem { get; set; }

		public StoreTransType StoreTransType { get; set; }

		
		public decimal Qty { get; set; }

		
		public decimal UnitPrice { get; set; }

		public long PurchaseId { get; set; }

		[ForeignKey("PurchaseId")]
		public Purchase PurchaseDetails { get; set; }

		
		public decimal QtyBalanceAfter { get; set; }

		public long InvoiceNum { get; set; }

		[ForeignKey("InvoiceNum")]
		public Invoices Invoice { get; set; }
	}
}
