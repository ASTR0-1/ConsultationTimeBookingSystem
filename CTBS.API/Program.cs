using CTBS.API.Extensions;
using CTBS.Application.Interfaces;
using CTBS.Application.Mappings;
using CTBS.Domain.Enums;
using CTBS.Infrastructure;
using CTBS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CTBS.API;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var configuration = builder.Configuration;

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.ConfigureSwagger();

		builder.Services.AddAuthentication();
		builder.Services.AddAuthorization();

		builder.Services.ConfigureInfrastructure(configuration);

		builder.Services.ConfigureJwt(configuration);
		builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();

		builder.Services.AddAutoMapper(typeof(MappingProfile));

		var app = builder.Build();

		await using var scope = app.Services.CreateAsyncScope();
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
		await CreateRolesAsync(roleManager);

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors(x => x
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		await app.RunAsync();
	}

	private static async Task CreateRolesAsync(RoleManager<IdentityRole<int>> roleManager)
	{
		if (!await roleManager.RoleExistsAsync(UserType.Lecturer.ToString()))
			await roleManager.CreateAsync(new IdentityRole<int>(UserType.Lecturer.ToString()));

		if (!await roleManager.RoleExistsAsync(UserType.Student.ToString()))
			await roleManager.CreateAsync(new IdentityRole<int>(UserType.Student.ToString()));
	}
}