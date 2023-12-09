using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class StoreItemWithSN : BaseModel
	{
		public long StoreItemId { get; set; }

		[ForeignKey("StoreItemId")]
		public StoreItem StoreItem { get; set; }

		public long TransactionId { get; set; }

		[ForeignKey("TransactionId")]
		public StoreTransaction StoreTransaction { get; set; }

		public string SerialNumber { get; set; }
	}
}
