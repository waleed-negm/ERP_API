namespace Application.DTOs
{
	public class CheckInBankDetails
	{
		public int Id { get; set; }
		public bool Selected { get; set; }
		public string ClientName { get; set; }
		public string CheckNum { get; set; }
		public decimal CheckAmount { get; set; }
		public string DueDate { get; set; }
		public string OrginalBank { get; set; }
		public string CheckStatus { get; set; }
		public string BankAccountNumber { get; set; }
		public string BankAccountName { get; set; }
	}
}
