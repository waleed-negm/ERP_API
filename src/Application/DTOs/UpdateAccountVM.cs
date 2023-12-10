using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class UpdateAccountVM
	{
		public string AccNum { get; set; }
		public decimal Balance { get; set; }
		[Required, StringLength(150)]
		public string AccountName { get; set; }
		[Required, StringLength(150)]
		public string AccountNameAr { get; set; }
		[Required]
		public int CurrencyId { get; set; }
		[Required]
		public int BranchId { get; set; }
		public bool IsActive { get; set; }
	}
}
