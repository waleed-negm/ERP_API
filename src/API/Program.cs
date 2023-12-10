using API.DIExtensions;
using API.Extensions;
using API.Filters;
using Application.DIExtensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.DIExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Application.Interfaces;
using API.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
builder.Services.AddSQLServerDatabase(builder.Configuration);
builder.Services.AddApplicationDependancies();
builder.Services.AddApplicationConfiguration(builder.Configuration);

builder.Services.AddControllers(options => options.Filters.Add<ExceptionActionFilter>())
	.AddFluentValidation(options =>
	{
		ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
		ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
		options.RegisterValidatorsFromAssembly(typeof(DependanciesExtension).Assembly);
	})
	.ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = context => context.HandleInvalidRequest())
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

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

//using (var scope = app.Services.CreateScope())
//{
//var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//dataContext.Database.Migrate();
//}
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<LogExceptionMiddlware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthorizationMiddleware>();
void useStaticFiles(WebApplication app)
{
	string imagesFolder = Path.Combine(app.Environment.ContentRootPath, "staticFiles");
	if (!Directory.Exists(imagesFolder))
	{
		Directory.CreateDirectory(imagesFolder);
	}
	app.UseStaticFiles(new StaticFileOptions()
	{
		FileProvider = new PhysicalFileProvider(imagesFolder),
		RequestPath = new PathString("/staticFiles")
	});
}
useStaticFiles(app);

async void SeedData(IHost app)
{
	IServiceScopeFactory serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
	using (IServiceScope Scope = serviceScopeFactory.CreateScope())
	{
		IDataSeedingService service = Scope.ServiceProvider.GetService<IDataSeedingService>();
		await service.SeedDataAsync();
	}
}
SeedData(app);
app.MapControllers();
app.MapGet("/", async context => { await context.Response.WriteAsync("CORE SYNC API."); });
app.Run();


