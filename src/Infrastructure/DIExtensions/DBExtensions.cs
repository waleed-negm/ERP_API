using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DIExtensions
{
	public static class DBExtensions
	{
		public static IServiceCollection AddSQLServerDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
				{
					builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
				});
			});
			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

			services.AddIdentity<ApplicationUser, IdentityRole>(o => o.Password = new PasswordOptions
			{
				RequireDigit = true,
				RequiredLength = 8,
				RequireLowercase = true,
				RequireUppercase = true,
				RequireNonAlphanumeric = false
			}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			return services;
		}
	}
}
