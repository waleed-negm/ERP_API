namespace Application.DTOs
{
	public class EmployeeSalary
	{
		public long EmployeeId { get; set; }
		public string Name { get; set; }
		public decimal BasicSalary { get; set; }
		public decimal Allowonces { get; set; }
		public decimal Overtime { get; set; }
		public decimal Commision { get; set; }
		public decimal InsuranceEmployer { get; set; }
		public decimal GrossIncome { get; set; }
		public decimal InsuranceEmployee { get; set; }
		public decimal Tax { get; set; }
		public decimal StaffAdvance { get; set; }
		public decimal Penalties { get; set; }
		public decimal TotalDeductions { get; set; }
		public decimal NetIncome { get; set; }
	}
}
