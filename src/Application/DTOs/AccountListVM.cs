namespace Application.DTOs
{
	public class AccountListVM
	{
		public string AccNum { get; set; }
		public string AccountName { get; set; }
		public string AccountNameAr { get; set; }
		public string AccTypeName { get; set; }
		public decimal Balance { get; set; }
		public decimal StartingBalance { get; set; }
		public bool IsParent { get; set; }
		public string CurrencyAbbr { get; set; }
		public bool IsActive { get; set; }
		public string BranchName { get; set; }

	}
}
