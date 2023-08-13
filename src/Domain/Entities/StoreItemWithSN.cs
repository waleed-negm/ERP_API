using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Main
{
	[Table("Finance_CurrentAsset_Inventory_Main_StoreItemWithSN")]

	public class StoreItemWithSN
	{
		public int Id { get; set; }
		public int StoreItemId { get; set; }
		[ForeignKey("StoreItemId")]
		public StoreItem StoreItem { get; set; }
		public int TransactionId { get; set; }
		[ForeignKey("TransactionId")]
		public StoreTransaction StoreTransaction { get; set; }
		public string SerialNumber { get; set; }
	}
}
