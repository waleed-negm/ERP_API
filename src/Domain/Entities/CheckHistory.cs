using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.CurrentAssetModules.ChecksModule.Model
{
	[Table("Finance_CurrentAsset_Checks_History")]
	public class CheckHistory
	{
		public int Id { get; set; }
		[Required, StringLength(255)]
		public string ChkNum { get; set; }
		[StringLength(255)]
		public string TransID { get; set; }
		public DateTime TransDate { get; set; }
		public string Description { get; set; }
	}
}
