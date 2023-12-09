using Domain.Entities.common;

namespace Domain.Entities
{
	public class SalaryBatch : BaseModel
	{
		public int BatchMonth { get; set; }

		public int BatchYear { get; set; }

		public string? TransId { get; set; }
	}
}
