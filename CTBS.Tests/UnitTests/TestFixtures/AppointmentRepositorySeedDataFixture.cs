using CTBS.Entities;
using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Tests.UnitTests.TestFixtures;

public class AppointmentRepositorySeedDataFixture : IDisposable
{
	public AppointmentRepositorySeedDataFixture()
	{
		ApplicationContext.Appointments.AddRange(new List<Appointment>
		{
			new()
			{
				Id = 1,
				Date = new DateOnly(2023, 12, 24),
				LecturerId = 1,
				StudentId = 1,
				Priority = 1
			},
			new()
			{
				Id = 2,
				Date = new DateOnly(2023, 12, 25),
				LecturerId = 2,
				StudentId = 2,
				Priority = 2
			}
		});
		ApplicationContext.SaveChanges();
	}

	public ApplicationContext ApplicationContext { get; } = new(
		new DbContextOptionsBuilder<ApplicationContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options);

	public void Dispose()
	{
		ApplicationContext.Database.EnsureDeleted();
		ApplicationContext.Dispose();
	}
}