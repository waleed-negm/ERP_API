using Application.BusinessLogic.CRM.Model;
using Application.BusinessLogic.ERPSettings.Model;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.Model
{
	[Table("Finance_CurrentLiabilties_NP_NotesPayable")]
	public class NotesPayable
	{
		[Key, StringLength(25)]
		public string ChkNum { get; set; }

		public DateTime WritingDate { get; set; }

		public DateTime DueDate { get; set; }


		[Required, Range(0, 9999999999999999.99)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal AmountLocal { get; set; }


		[Required, Range(0, 9999999999999999.99)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal AmountForgin { get; set; }

		[Required]
		public int CurrencyId { get; set; }
		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }

		public string BankAccountNum { get; set; }

		[ForeignKey("BankAccountNum")]
		public AccountChart BankAccount { get; set; }

		[StringLength(15)]
		public int SupplierId { get; set; }
		[ForeignKey("SupplierId")]
		public Contacts Supplier { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Paid { get; set; }

		public NotesPayableStatusEnum CheckStatus { get; set; }
	}
}