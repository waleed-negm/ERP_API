using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;
using Domain.Enums;

namespace Domain.Entities
{
	public class Journal : BaseModel
	{
		[Key, Required, StringLength(15)]
		public string TransId { get; set; }

		[Required]
		public DateTime EntryDate { get; set; }


		public DateTime TransDate { get; set; }

		[Required]
		public string TransDesc { get; set; }

		public string DocName { get; set; }

		public long TransCount { get; set; }

		public SystemModulesEnum SystemModule { get; set; }

		public string UserName { get; set; }
	}
}
