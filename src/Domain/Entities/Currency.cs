using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class Currency : BaseModel
	{
		[Required, StringLength(25)]
		public string CurrencyName { get; set; }

		[Required, StringLength(25)]
		public string CurrencyNameAr { get; set; }

		[Required, StringLength(10)]
		public string CurrencyAbbrev { get; set; }

		public bool IsDefault { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Rate { get; set; }
	}
}
