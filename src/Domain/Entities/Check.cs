using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Check : BaseModel
	{
		[Required, StringLength(255)]
		public string ChkNum { get; set; }

		[Column(TypeName = "Date")]
		public DateTime DueDate { get; set; }

		[Required]
		[ForeignKey("CurrencyId")]
		public long CurrencyId { get; set; }
		public virtual Currency Currency { get; set; }

		[Required, Range(0, 9999999999999999.99)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal AmountLocal { get; set; }

		[Required, Range(0, 9999999999999999.99)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal AmountForgin { get; set; }

		[ForeignKey("ContactId")]
		public long ContactId { get; set; }
		public virtual Contacts Contact { get; set; }

		[StringLength(255)]
		public string OrginalBank { get; set; }

		// Collection Part
		[Range(0, 9999999999999999.99)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Paid { get; set; }

		[Range(0, 9999999999999999.99)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnPaid { get; set; }


		[ForeignKey("CheckStatusId")]
		public long CheckStatusId { get; set; }
		public virtual CheckStatus CheckStatus { get; set; }

		[ForeignKey("CheckLocationId")]
		public long CheckLocationId { get; set; }
		public virtual CheckLocation CheckLocation { get; set; }

		// Current Bank
		[StringLength(50)]
		public string BankAccNum { get; set; }

		[ForeignKey("BankAccNum")]
		public AccountChart BankAcc { get; set; }

		[ForeignKey("HafzaId")]
		public long HafzaId { get; set; }
		public virtual CheckHafza CheckHafza { get; set; }

	}
}
