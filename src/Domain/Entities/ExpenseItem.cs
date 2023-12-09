using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class ExpenseItem : BaseModel
	{
		public string ExpenseName { get; set; }

		public string AccNum { get; set; }

		[ForeignKey("AccNum")]
		public AccountChart AccountDetail { get; set; }

		public long ExpenseTypeId { get; set; }

		[ForeignKey("ExpenseTypeId")]
		public ExpenseType ExpenseType { get; set; }
	}
}
