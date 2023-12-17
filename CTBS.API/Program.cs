using CTBS.API.Extensions;
using CTBS.API.Utility;
using CTBS.Contracts;
using CTBS.Entities.Mappings;

namespace CTBS.API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var configuration = builder.Configuration;

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.ConfigureSwagger();

		builder.Services.AddAuthentication();
		builder.Services.ConfigureIdentity();
		builder.Services.ConfigureJwt(configuration);
		builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();

		builder.Services.ConfigureSqlContext(configuration);
		builder.Services.ConfigureRepositoryManager();
		builder.Services.AddAutoMapper(typeof(MappingProfile));

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}
