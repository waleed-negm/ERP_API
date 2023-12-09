using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class CheckStatus : BaseModel
	{
		[Required, StringLength(255)]
		public string CheckStatusEN { get; set; }

		[Required, StringLength(255)]
		public string CheckStatusAR { get; set; }

		public bool IsDefault { get; set; }
	}
}
