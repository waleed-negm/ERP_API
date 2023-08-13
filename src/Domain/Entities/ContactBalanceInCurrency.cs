using Application.BusinessLogic.ERPSettings.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CRM.Model
{
	[Table("CRM_ContactBalanceInCurrency")]
	public class ContactBalanceInCurrency
	{
		public int ContactId { get; set; }
		[ForeignKey("ContactId")]
		public Contacts Contacts { get; set; }

		public int CurrencyId { get; set; }
		[ForeignKey("CurrencyId")]
		public Currency Currency { get; set; }
		[StringLength(50)]
		public string AccNum { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Balance { get; set; }
	}
}
