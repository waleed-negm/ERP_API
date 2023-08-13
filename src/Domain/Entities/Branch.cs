using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BusinessLogic.ERPSettings.Model
{
	[Table("Finance_Settings_Branch")]
	public class Branch
	{
		public int Id { get; set; }
		[Required, StringLength(255)]
		public string BranchName { get; set; }
	}
}
