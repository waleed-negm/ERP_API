using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class CheckHafza : BaseModel
	{
		[StringLength(255)]
		public string HafzaName { get; set; }

		[StringLength(50)]
		public string BankAccNum { get; set; }

		public DateTime HafzaDate { get; set; }
	}
}
