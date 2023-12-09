using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Brand : BaseModel
	{
		[Required, StringLength(50)]
		public string BrandName { get; set; }
	}
}
