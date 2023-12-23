using CTBS.Entities;
using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Tests.UnitTests.TestFixtures;

public class QuestionsCategoryRepositorySeedDataFixture : IDisposable
{
	public QuestionsCategoryRepositorySeedDataFixture()
	{
		ApplicationContext.QuestionsCategories.AddRange(new List<QuestionsCategory>
		{
			new()
			{
				Id = 1,
				Name = "Category1"
			},
			new()
			{
				Id = 2,
				Name = "Category2"
			},
			new()
			{
				Id = 4,
				Name = "Category4"
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