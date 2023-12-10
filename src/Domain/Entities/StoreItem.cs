using Domain.Entities.common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class StoreItem : BaseModel
	{
		[StringLength(100)]
		public string BarCode { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Required, StringLength(50)]
		public string NameAr { get; set; }

		public long ProductTypeId { get; set; }

		[ForeignKey("ProductTypeId")]
		public ProductType ProductType { get; set; }

		public long UnitMeasureId { get; set; }

		[ForeignKey("UnitMeasureId")]
		public UnitMeasure UnitMeasure { get; set; }

		public long BrandId { get; set; }

		[ForeignKey("BrandId")]
		public Brand Brand { get; set; }


		public decimal Qty { get; set; }


		public decimal Balance { get; set; }

		public bool WithSN { get; set; }

		public StoreSystem StoreSystem { get; set; }

		public string StoreAccNum { get; set; }

		[ForeignKey("StoreAccNum")]
		public AccountChart StoreAccDetails { get; set; }

		public string PurchaseAccNum { get; set; }

		[ForeignKey("PurchaseAccNum")]
		public AccountChart PurchseAccDetials { get; set; }
	}
}
