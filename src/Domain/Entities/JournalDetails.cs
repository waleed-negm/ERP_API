using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Finance_GL_JournalDetails")]
	public class JournalDetails : BaseModel
	{
		[StringLength(15)]
		[ForeignKey("TransId")]
		public string TransId { get; set; }
		public virtual Journal Trans { get; set; }

		[StringLength(15)]
		public string AccNum { get; set; }

		
		[Range(0, 9999999999999999.99)]
		public decimal Amount { get; set; }

		[Range(0, 9999999999999999.99)]
		
		public decimal AmountLocal { get; set; }

		public TransactionSidesEnum Side { get; set; }

		
		[Range(0, 9999999999999999.99)]
		public decimal BalanceAfter { get; set; }

		[Required]
		public long CurrencyId { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }

		
		[Range(0, 9999999999999999.99)]
		public decimal UsedRate { get; set; }
	}
}
