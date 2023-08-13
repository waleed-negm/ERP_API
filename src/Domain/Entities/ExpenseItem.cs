using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.PurchasesModule.Model
{
	[Table("Finance_Expense_ExpenseItem")]
	public class ExpenseItem
	{
		public int Id { get; set; }
		public string ExpenseName { get; set; }
		public string AccNum { get; set; }
		[ForeignKey("AccNum")]
		public AccountChart AccountDetail { get; set; }

		public int ExpenseTypeId { get; set; }
		[ForeignKey("ExpenseTypeId")]
		public ExpenseType ExpenseType { get; set; }
	}
}
