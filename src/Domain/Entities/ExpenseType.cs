using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.PurchasesModule.Model
{
	[Table("Finance_Expense_ExpenseType")]
	public class ExpenseType
	{
		public int Id { get; set; }
		[Required, StringLength(75)]
		public string ExpenseTypeName { get; set; }
	}
}
