using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("HR_SalaryBatch")]
	public class SalaryBatch
	{
		public int Id { get; set; }
		public int BatchMonth { get; set; }
		public int BatchYear { get; set; }
		public string TransId { get; set; }
	}
}
