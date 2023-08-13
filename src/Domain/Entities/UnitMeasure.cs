using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Model.Settings
{
	[Table("Finance_CurrentAsset_Inventory_Settings_UnitMeasure")]
	public class UnitMeasure
	{
		public int Id { get; set; }
		[Required, StringLength(50)]
		public string UnitName { get; set; }
	}
}
