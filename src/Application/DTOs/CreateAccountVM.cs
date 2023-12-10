using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class CreateAccountVM
	{
		[Required, StringLength(150)]
		public string AccountName { get; set; }
		[Required, StringLength(150)]
		public string AccountNameAr { get; set; }
		[Required]
		public int AccTypeId { get; set; }
		[Required]
		public int CurrencyId { get; set; }
		[Required]
		public int BranchId { get; set; }
	}
}
