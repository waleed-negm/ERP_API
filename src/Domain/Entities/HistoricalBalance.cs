using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class HistoricalBalance : BaseModel
	{
		public long FinancialPeriodId { get; set; }

		[ForeignKey("FinancialPeriodId")]
		public FiniacialPeriod FiniacialPeriod { get; set; }

		[Required, StringLength(50)]
		public string AccNum { get; set; }

		public AccountChart AccountDetails { get; set; }

		
		public decimal Balance { get; set; }

		
		public decimal UsedRate { get; set; }

	}
}
