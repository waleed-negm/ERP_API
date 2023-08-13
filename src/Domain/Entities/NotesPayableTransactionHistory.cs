using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.Model
{
	[Table("Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory")]
	public class NotesPayableTransactionHistory
	{
		public int Id { get; set; }
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
