namespace Domain.Entities.Interfaces
{
	public interface IReviewableEntity
	{
		public DateTime ReviewedAt { get; set; }
		public string ReviewedBy { get; set; }
	}
}
