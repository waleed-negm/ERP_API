namespace Domain.Entities.Interfaces
{
	public interface IEvaluatableEntity
	{
		public DateTime EvaluatedAt { get; set; }
		public string EvaluatedById { get; set; }
	}
}
