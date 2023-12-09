using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;
using Domain.Enums;

namespace Domain.Entities
{
	public class AccountChartCounter : BaseModel
	{
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
