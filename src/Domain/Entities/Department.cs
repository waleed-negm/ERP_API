using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Department : BaseModel
	{
		[Required, StringLength(50)]
		public string DepartmentName { get; set; }
	}
}
