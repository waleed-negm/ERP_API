using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class HafzaDetails
	{
		public int Id { get; set; }
		[Required]
		public string BankAccNum { get; set; }
		[Required]
		public string HafzaDate { get; set; }
		[Required, StringLength(255)]
		public string HafzaName { get; set; }
	}
}
