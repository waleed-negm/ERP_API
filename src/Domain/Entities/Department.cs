using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("HR_Department")]
	public class Department
	{
		public int Id { get; set; }
		[Required, StringLength(50)]
		public string DepartmentName { get; set; }
	}
}
