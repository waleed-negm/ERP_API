using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class CheckHistory : BaseModel
	{
		[Required, StringLength(255)]
		public string ChkNum { get; set; }

		[StringLength(255)]
		public string TransID { get; set; }

		public DateTime TransDate { get; set; }

		public string Description { get; set; }
	}
}
