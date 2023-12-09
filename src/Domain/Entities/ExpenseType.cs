using System.ComponentModel.DataAnnotations;
using Domain.Entities.common;

namespace Domain.Entities
{
	public class ExpenseType : BaseModel
	{
		[Required, StringLength(75)]
		public string ExpenseTypeName { get; set; }
	}
}
