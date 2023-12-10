namespace Application.DTOs
{
	public class ExpenseDetailsVM
	{
		public int ExpenseItemId { get; set; }
		public int? SupplierId { get; set; }
		public string ExpenseDate { get; set; }
		public decimal Amount { get; set; }
		public int CurrencyId { get; set; }
		public int? CostCenterId { get; set; }
	}
}
