using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace API.DIExtensions
{
	public static class SwaggerExtension
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(
			options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization, Enter bearer token",
					Name = "JWT Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					BearerFormat = "JWT"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Id = JwtBearerDefaults.AuthenticationScheme,
								Type = ReferenceType.SecurityScheme
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
			});
			return services;
		}
	}
}
