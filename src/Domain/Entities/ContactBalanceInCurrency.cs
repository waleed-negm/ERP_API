using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class ContactBalanceInCurrency : BaseModel
	{
		public long ContactId { get; set; }

		[ForeignKey("ContactId")]
		public Contacts Contacts { get; set; }

		public long CurrencyId { get; set; }

		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }

		[StringLength(50)]
		public string AccNum { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Balance { get; set; }
	}
}
