using Application.BusinessLogic.CRM.Model;
using Application.BusinessLogic.ERPSettings.Model;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Model;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.SalesModule.Model
{
	[Table("Finance_Sales_ClientTransaction")]
	public class ClientTransaction
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		[ForeignKey("ClientId")]
		public Contacts ClientDetails { get; set; }
		public string InvoiceNum { get; set; }
		[ForeignKey("InvoiceNum")]
		public Invoices InvoiceDetails { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal PaymentAmount { get; set; }

		[Column(TypeName = "Date")]
		public DateTime PaymentDate { get; set; }
		public int CurrencyId { get; set; }
		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }

		public string PaymentAccNum { get; set; }
		[ForeignKey("PaymentAccNum")]
		public AccountChart PaymentAccDetails { get; set; }
		public ClientPaymentMethod PaymentMenthod { get; set; }

		public string TransId { get; set; }
		[ForeignKey("TransId")]
		public Journal TransactionDetails { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal BalanceAfter { get; set; }
	}
}
