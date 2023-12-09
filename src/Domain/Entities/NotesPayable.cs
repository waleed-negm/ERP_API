using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class NotesPayable : BaseModel
	{
		[StringLength(25)]
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
		public long CurrencyId { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }

		public string BankAccountNum { get; set; }

		[ForeignKey("BankAccountNum")]
		public AccountChart BankAccount { get; set; }

		public long SupplierId { get; set; }

		[ForeignKey("SupplierId")]
		public Contacts Supplier { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Paid { get; set; }

		public NotesPayableStatusEnum CheckStatus { get; set; }
	}
}
