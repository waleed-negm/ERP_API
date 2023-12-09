using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class FiniacialPeriod : BaseModel
	{
		[Required, StringLength(50)]
		public string YearName { get; set; }

		public string? StartDate { get; set; }

		public string? EndDate { get; set; }

		public bool IsActive { get; set; }
	}
}
