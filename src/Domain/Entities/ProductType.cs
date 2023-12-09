using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class ProductType : BaseModel
	{
		[Required, StringLength(50)]
		public string ProductTypeName { get; set; }
	}
}
