using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class ExpenseSummary : BaseModel
	{
		public long ExpenseItemId { get; set; }

		public long SupplierId { get; set; }

		public DateTime ExpenseDate { get; set; }


		public decimal Amount { get; set; }

		public long CurrencyId { get; set; }


		public decimal LocalAmount { get; set; }

		public long CostCenterId { get; set; }

		// Mapping Props
		[ForeignKey("ExpenseItemId")]
		public ExpenseItem ExpenseItem { get; set; }

		[ForeignKey("SupplierId")]
		public Contacts Supplier { get; set; }

		[ForeignKey("CostCenterId")]
		public Department Department { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }
	}
}
