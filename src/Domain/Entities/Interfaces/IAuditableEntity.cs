namespace Domain.Entities.Interfaces
{
	public interface IAuditableEntity
	{
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string CreatedById { get; set; }
		public string UpdatedById { get; set; }
	}
}
