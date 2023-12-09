using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class AccountChart : BaseModel
	{
		[Key, StringLength(50)]
		public string AccNum { get; set; }

		[Required, StringLength(150)]
		public string AccountName { get; set; }

		[StringLength(150)]
		public string AccountNameAr { get; set; }

		[ForeignKey("AccTypeId")]
		public long AccTypeId { get; set; }
		public virtual AccountChartCounter AccType { get; set; }

		public AccountNatureEnum AccountNature { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Balance { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? StartingBalance { get; set; }

		public bool IsParent { get; set; }

		[Required]
		[ForeignKey("CurrencyId")]
		public long CurrencyId { get; set; }
		public virtual Currency Currency { get; set; }

		[StringLength(50)]
		public string ParentAcNum { get; set; }

		[DefaultValue(true)]
		public bool IsActive { get; set; }

		[ForeignKey("BranchId")]
		public long BranchId { get; set; }
		public virtual Branch Branch { get; set; }
	}
}
