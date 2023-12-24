using CTBS.API;
using CTBS.Domain.Models;
using CTBS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CTBS.Tests.IntegrationTests.TestConfigurations;

public class ApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			var descriptor = services.SingleOrDefault(
				d => d.ServiceType ==
				     typeof(DbContextOptions<ApplicationContext>));

			services.Remove(descriptor);

			services.AddDbContext<ApplicationContext>(options =>
			{
				options.UseInMemoryDatabase("InMemoryDbForTesting");
			});

			var sp = services.BuildServiceProvider();

			using var scope = sp.CreateScope();
			var scopedServices = scope.ServiceProvider;
			var db = scopedServices.GetRequiredService<ApplicationContext>();

			ConfigureUsers(db);
			ConfigureQuestionCategories(db);
			ConfigureAppointments(db);

			db.Database.EnsureCreated();
		});
	}

	private static void ConfigureUsers(ApplicationContext context)
	{
		context.Users.Add(new User
		{
			FirstName = "",
			MiddleName = "",
			LastName = ""
		});
		context.Users.Add(new User
		{
			FirstName = "",
			MiddleName = "",
			LastName = ""
		});

		context.SaveChanges();
	}

	private static void ConfigureQuestionCategories(ApplicationContext context)
	{
		context.QuestionsCategories.Add(new QuestionsCategory
		{
			ImpactOnAmountOfTime = 1,
			Name = "Name"
		});
		context.QuestionsCategories.Add(new QuestionsCategory
		{
			ImpactOnAmountOfTime = 1,
			Name = "Name"
		});

		context.SaveChanges();
	}

	private static void ConfigureAppointments(ApplicationContext context)
	{
		context.Appointments.Add(new Appointment());
		context.Appointments.Add(new Appointment());

		context.SaveChanges();
	}
}