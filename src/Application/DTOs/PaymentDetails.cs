using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class PaymentDetails
	{
		[Required]
		public SupplierPaymentMethod PaymentMethod { get; set; } = SupplierPaymentMethod.Safe;
		public decimal PaymentAmount { get; set; }
		[Required]
		public string PaymentDate { get; set; }
		public string SafeAccNum { get; set; }
		public string BankAccNum { get; set; }
		public string CustodyAccNum { get; set; }
		public string CheckNum { get; set; }
		public string WritingDate { get; set; }
		public string PaymentDueDate { get; set; }
		public string Description { get; set; }
		public string RecieptNumber { get; set; }
		public bool IsSafe { get; set; } = true;
		public bool IsBank { get; set; }
		public bool IsCheck { get; set; }
		public bool IsCustody { get; set; }
	}
}
