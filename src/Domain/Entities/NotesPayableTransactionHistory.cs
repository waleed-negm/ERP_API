using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;
using Domain.Enums;

namespace Domain.Entities
{
	public class NotesPayableTransactionHistory : BaseModel
	{
		[Required]
		public string ChkNum { get; set; }

		public string TransId { get; set; }

		public DateTime ActionDate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal PaidAmount { get; set; }

		public NotesPayableStatusEnum StatusAfterAction { get; set; }

		public string Description { get; set; }

		[ForeignKey("ChkNum")]
		public NotesPayable ChkDetails { get; set; }
	}
}
