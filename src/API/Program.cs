using API.Configuration;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), builder =>
{
	_ = builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
}));

builder.Services.AddControllers();//.AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
}
);


builder.Services.AddServices();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(o =>
  {
	  o.RequireHttpsMetadata = false;
	  o.SaveToken = false;
	  o.TokenValidationParameters = new TokenValidationParameters
	  {
		  ValidateIssuerSigningKey = true,
		  ValidateIssuer = true,
		  ValidateAudience = true,
		  ValidateLifetime = true,
		  ValidIssuer = "coresync",
		  ValidAudience = "coresync",
		  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7LOak9a8MQS1uTLpMa16z6e8AtzoeDFCCYTDz0p8V0k")),
		  ClockSkew = TimeSpan.Zero
	  };
  });
WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	dataContext.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


