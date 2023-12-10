using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class SupplierTransaction : BaseModel
	{
		public long SupplierId { get; set; }

		[ForeignKey("SupplierId")]
		public Contacts SupplierDetails { get; set; }

		public long PurchaseId { get; set; }

		[ForeignKey("PurchaseId")]
		public Purchase PurchaseDetails { get; set; }

		
		public decimal PaymentAmount { get; set; }

		
		public DateTime PaymentDate { get; set; }

		public long CurrencyId { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }

		public string PaymentAccNum { get; set; }

		[ForeignKey("PaymentAccNum")]
		public AccountChart PaymentAccDetails { get; set; }

		public SupplierPaymentMethod PaymentMenthod { get; set; }

		public string TransId { get; set; }

		[ForeignKey("TransId")]
		public Journal TransactionDetails { get; set; }

		
		public decimal BalanceAfter { get; set; }

	}
}
