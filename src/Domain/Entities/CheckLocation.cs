using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class CheckLocation : BaseModel
	{
		[Required, StringLength(255)]
		public string CheckLocationEN { get; set; }

		[Required, StringLength(255)]
		public string CheckLocationAR { get; set; }

		public bool IsDefualt { get; set; }
	}
}
