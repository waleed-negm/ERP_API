using Application.BusinessLogic.PurchasesModule.Model;
using Application.BusinessLogic.SalesModule.Model;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Main
{
	[Table("Finance_CurrentAsset_Inventory_Main_StoreTransaction")]

	public class StoreTransaction
	{
		public int Id { get; set; }
		public int StoreItemId { get; set; }
		[ForeignKey("StoreItemId")]
		public StoreItem StoreItem { get; set; }
		public StoreTransType StoreTransType { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Qty { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; set; }
		public int PurchaseId { get; set; }
		[ForeignKey("PurchaseId")]
		public Purchase PurchaseDetails { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal QtyBalanceAfter { get; set; }


		[StringLength(6)]
		public string InvoiceNum { get; set; }
		[ForeignKey("InvoiceNum")]
		public Invoices Invoice { get; set; }

	}
}
