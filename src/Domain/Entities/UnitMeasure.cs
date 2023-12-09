using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class UnitMeasure : BaseModel
	{
		[Required, StringLength(50)]
		public string UnitName { get; set; }
	}
}
