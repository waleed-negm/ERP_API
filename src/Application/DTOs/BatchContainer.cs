using Domain.Entities;

namespace Application.DTOs
{
	public class BatchContainer
	{
		public string AccNum { get; set; }
		public string PayrollDate { get; set; }
		public SalaryBatch SalaryBatch { get; set; } = new SalaryBatch();
		public List<EmployeeSalary> EmployeeSalaries { get; set; } = new List<EmployeeSalary>();
	}
}
