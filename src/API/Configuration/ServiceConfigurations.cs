using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
	public static class ServiceConfigurations
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Program));
			services.AddHttpContextAccessor();
			services.AddIdentity<IdentityUser, IdentityRole>(o => o.Password = new PasswordOptions
			{
				RequireDigit = true,
				RequiredLength = 8,
				RequireLowercase = true,
				RequireUppercase = true,
				RequireNonAlphanumeric = false
			}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

		}
	}
}
