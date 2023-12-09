namespace Application.BusinessLogic.SalesModule.ViewModel.ClientStatment
{
	public class StatmentTransactions
	{
		public string TransDate { get; set; }
		public decimal Debit { get; set; }
		public decimal Credit { get; set; }
		public string Description { get; set; }
		public decimal BalanceAfter { get; set; }
		public long? InvoiceNum { get; set; }
	}
}
