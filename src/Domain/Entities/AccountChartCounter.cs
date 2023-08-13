using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model
{
	[Table("Finance_GL_AccountChartCounter")]
	public class AccountChartCounter
	{
		[Key]
		public int Id { get; set; }

		[Required, StringLength(50)]
		public string AccountType { get; set; }

		public AccountCategoryEnum AccountCategory { get; set; }


		[StringLength(50)]
		public string ParentAccNum { get; set; }
		public int Count { get; set; }

		public bool BalanceSheet { get; set; }

		public bool IncomeStatement { get; set; }

	}
}