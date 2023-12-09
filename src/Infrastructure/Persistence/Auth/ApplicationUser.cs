
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Auth
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Position { get; set; }
		public string Title { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public bool IsDeleted { get; set; }
	}
}
