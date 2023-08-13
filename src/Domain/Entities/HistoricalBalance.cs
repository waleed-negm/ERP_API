using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model
{
	[Table("Finance_GL_HistoricalBalance")]
	public class HistoricalBalance
	{
		public int Id { get; set; }
		public int FinancialPeriodId { get; set; }
		[ForeignKey("FinancialPeriodId")]
		public FiniacialPeriod FiniacialPeriod { get; set; }
		[Required, StringLength(50)]
		public string AccNum { get; set; }

		public AccountChart AccountDetails { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Balance { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal UsedRate { get; set; }

	}
}
