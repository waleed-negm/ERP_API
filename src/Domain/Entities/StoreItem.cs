using Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Settings;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Model;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Main
{
	[Table("Finance_CurrentAsset_Inventory_Main_StoreItem")]
	public class StoreItem
	{
		public int Id { get; set; }
		[StringLength(100)]
		public string BarCode { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }
		[Required, StringLength(50)]
		public string NameAr { get; set; }
		public int ProductTypeId { get; set; }
		[ForeignKey("ProductTypeId")]
		public ProductType ProductType { get; set; }
		public int UnitMeasureId { get; set; }
		[ForeignKey("UnitMeasureId")]
		public UnitMeasure UnitMeasure { get; set; }
		public int BrandId { get; set; }
		[ForeignKey("BrandId")]
		public Brand Brand { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Qty { get; set; }
		[Column(TypeName = "decimal(18,2)")]
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
