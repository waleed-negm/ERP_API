using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class StoreItemBalanceDetails : BaseModel
	{
		public long StoreItemId { get; set; }

		[ForeignKey("StoreItemId")]
		public StoreItem StoreItem { get; set; }

		[ForeignKey("StoreTransactionId")]
		public long StoreTransactionId { get; set; }

		public StoreTransaction StoreTransaction { get; set; }

		
		public decimal CurrentBalance { get; set; }

		
		public DateTime ExpiryDate { get; set; }



	}
}
