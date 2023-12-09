using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Branch : BaseModel
	{
		[Required, StringLength(255)]
		public string BranchName { get; set; }
	}
}
