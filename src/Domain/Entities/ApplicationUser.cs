using Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Auth
{
	public class ApplicationUser : IdentityUser, IAuditableEntity, ISoftDeletedEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Position { get; set; }
		public string Title { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string CreatedById { get; set; }
		public string? UpdatedById { get; set; }

		public DateTime? DeletedAt { get; set; }
		public string DeletedById { get; set; }
		public virtual bool IsDeleted { get => DeletedAt.HasValue || DeletedById is not null; }
	}
}
