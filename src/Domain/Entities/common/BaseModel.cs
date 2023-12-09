using System.ComponentModel;

namespace Domain.Entities.common
{
	public class BaseModel
	{
		public long Id { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string? CreatedBy { get; set; }
		public string? ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		[DefaultValue(false)]
		public bool IsDeleted { get; set; }
	}
}
