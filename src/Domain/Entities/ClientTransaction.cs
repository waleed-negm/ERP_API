using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class ClientTransaction : BaseModel
	{
		public long ClientId { get; set; }

		[ForeignKey("ClientId")]
		public virtual Contacts ClientDetails { get; set; }


		[ForeignKey("InvoiceNum")]
		public long? InvoiceNum { get; set; }
		public virtual Invoices InvoiceDetails { get; set; }

		
		public decimal PaymentAmount { get; set; }

		
		public DateTime PaymentDate { get; set; }

		[ForeignKey("CurrencyId")]
		public long CurrencyId { get; set; }
		public virtual Currency Currency { get; set; }


		[ForeignKey("PaymentAccNum")]
		public string PaymentAccNum { get; set; }
		public virtual AccountChart PaymentAccDetails { get; set; }

		public ClientPaymentMethod PaymentMenthod { get; set; }


		[ForeignKey("TransId")]
		public string TransId { get; set; }
		public virtual Journal TransactionDetails { get; set; }

		
		public decimal BalanceAfter { get; set; }
	}
}
