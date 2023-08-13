using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("HR_Employee")]
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Title { get; set; }
		public int DepartmentId { get; set; }
		[ForeignKey("DepartmentId")]
		public Department Department { get; set; }
		public decimal BasicSalary { get; set; }
		public decimal InsuranceSalary { get; set; }

		public string StaffAdvanceAccNum { get; set; }
	}
}