namespace Domain.Entities.Interfaces
{
	public interface ISoftDeletedEntity
	{
		public DateTime? DeletedAt { get; set; }
		public string? DeletedById { get; set; }
		public virtual bool IsDeleted { get => false; }
	}
}
