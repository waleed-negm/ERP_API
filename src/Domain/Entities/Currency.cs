using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.ERPSettings.Model
{
	[Table("Finance_Settings_Currency")]
	public class Currency
	{
		public int Id { get; set; }
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
