using Domain.Entities.Interfaces;

namespace Domain.Entities.common
{
	public class BaseModel : IAuditableEntity, ISoftDeletedEntity
	{
		public long Id { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string CreatedById { get; set; }
		public string? UpdatedById { get; set; }

		public DateTime? DeletedAt { get; set; }
		public string? DeletedById { get; set; }

		public virtual bool IsDeleted { get => DeletedAt.HasValue || DeletedById is not null; }
	}
}
