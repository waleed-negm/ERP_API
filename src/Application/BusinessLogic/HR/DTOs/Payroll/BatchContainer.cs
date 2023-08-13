using Domain.Entities;

namespace Application.BusinessLogic.HR.DTOs.Payroll
{
	public class BatchContainer
	{
		public BatchContainer()
		{
			SalaryBatch = new SalaryBatch();
			EmployeeSalaries = new List<EmployeeSalary>();
		}
		public string AccNum { get; set; }
		public string PayrollDate { get; set; }
		public SalaryBatch SalaryBatch { get; set; }
		public List<EmployeeSalary> EmployeeSalaries { get; set; }
	}
}
