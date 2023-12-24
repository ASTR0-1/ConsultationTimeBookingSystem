using CTBS.Application.Interfaces;
using CTBS.Domain.Models;
using CTBS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CTBS.Infrastructure;

public static class ServiceExtensions
{
	public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.ConfigureSqlContext(configuration);
		services.ConfigureRepositoryManager();
		services.ConfigureIdentity();
	}

	private static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationContext>(opts =>
			opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
				b => b.MigrationsAssembly("CTBS.Infrastructure")));
	}

	private static void ConfigureRepositoryManager(this IServiceCollection services)
	{
		services.AddScoped<IRepositoryManager, RepositoryManager>();
	}

	private static void ConfigureIdentity(this IServiceCollection services)
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
			.AddRoleManager<RoleManager<IdentityRole<int>>>();
	}
}