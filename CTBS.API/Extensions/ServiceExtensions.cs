using System.Reflection;
using System.Text;
using CTBS.Contracts;
using CTBS.Entities;
using CTBS.Entities.Models;
using CTBS.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CTBS.API.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
		services.AddDbContext<ApplicationContext>(opts =>
			opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("CTBS.API")));

	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();

	public static void ConfigureIdentity(this IServiceCollection services)
	{
		var builder = services.AddIdentityCore<User>(opts =>
		{
			opts.Password.RequireDigit = true;
			opts.Password.RequireLowercase = false;
			opts.Password.RequireUppercase = false;
			opts.Password.RequireNonAlphanumeric = false;
			opts.Password.RequiredLength = 10;
			opts.User.RequireUniqueEmail = true;
		});

		builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole<int>), builder.Services);
		builder.AddEntityFrameworkStores<ApplicationContext>()
			.AddDefaultTokenProviders();
	}

	public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection("JwtSettings");
		var secretKey = configuration.GetSection("JwtSecret").Value;

		services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(opts =>
			{
				opts.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
					ValidAudience = jwtSettings.GetSection("validAudience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
				};
			});
	}

	public static void ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Consultation time booking system", Version = "v1" });

			// Include the XML comments for Swagger
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			c.IncludeXmlComments(xmlPath);
		});
	}
}
