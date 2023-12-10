using Application.Configuration;
using Clients.EmailClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DIExtensions
{
	public static class ConfigurationExtinstion
	{
		public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ServiceConfiguration>(configuration.GetSection(nameof(ServiceConfiguration)));
			services.Configure<EmailConfiguration>(configuration.GetSection(nameof(EmailConfiguration)));
			services.Configure<JWTConfiguration>(configuration.GetSection(nameof(JWTConfiguration)));
			return services;
		}
	}
}
