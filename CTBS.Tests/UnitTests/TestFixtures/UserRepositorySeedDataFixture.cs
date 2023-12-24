using CTBS.Domain.Models;
using CTBS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Tests.UnitTests.TestFixtures;

public class UserRepositorySeedDataFixture : IDisposable
{
	public UserRepositorySeedDataFixture()
	{
		ApplicationContext.Users.AddRange(new List<User>
		{
			new()
			{
				Id = 1,
				FirstName = "Danil",
				MiddleName = "",
				LastName = ""
			},
			new()
			{
				Id = 2,
				FirstName = "Danil",
				MiddleName = "",
				LastName = ""
			},
			new()
			{
				Id = 300,
				FirstName = "Danil",
				MiddleName = "",
				LastName = ""
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