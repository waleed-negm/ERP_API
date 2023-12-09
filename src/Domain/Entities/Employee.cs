using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Employee : BaseModel
	{
		public string Name { get; set; }

		public string Phone { get; set; }

		public string Title { get; set; }

		public long DepartmentId { get; set; }

		[ForeignKey("DepartmentId")]
		public Department Department { get; set; }

		public decimal BasicSalary { get; set; }

		public decimal InsuranceSalary { get; set; }

		public string? StaffAdvanceAccNum { get; set; }
	}
}
