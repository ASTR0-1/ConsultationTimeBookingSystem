using CTBS.Contracts;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using CTBS.Repository;
using CTBS.Tests.UnitTests.TestFixtures;
using Xunit;

namespace CTBS.Tests.UnitTests;

public class QuestionsCategoryRepositoryTests : IClassFixture<QuestionsCategoryRepositorySeedDataFixture>
{
	private readonly QuestionsCategoryRepositorySeedDataFixture _fixture;
	private readonly IQuestionsCategoryRepository _questionsCategoryRepository;

	public QuestionsCategoryRepositoryTests(QuestionsCategoryRepositorySeedDataFixture fixture)
	{
		_fixture = fixture;
		_questionsCategoryRepository = new QuestionsCategoryRepository(_fixture.ApplicationContext);
	}

	[Fact]
	public async void GetAllQuestionsCategoriesAsync_ShouldReturnAllQuestionsCategories()
	{
		// Arrange
		var requestParameters = new RequestParameters {PageNumber = 1, PageSize = 10};

		// Act
		var result = await _questionsCategoryRepository.GetAllQuestionsCategoriesAsync(requestParameters, false);

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public async void GetQuestionsCategoryAsync_ShouldReturnQuestionsCategory_WhenQuestionsCategoryExists()
	{
		// Arrange
		var questionsCategoryId = 1;

		// Act
		var result = await _questionsCategoryRepository.GetQuestionsCategoryAsync(questionsCategoryId, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(questionsCategoryId, result.Id);
	}

	[Fact]
	public async void GetQuestionsCategoryAsync_ShouldReturnNull_WhenQuestionsCategoryDoesNotExist()
	{
		// Arrange
		var questionsCategoryId = 999;

		// Act
		var result = await _questionsCategoryRepository.GetQuestionsCategoryAsync(questionsCategoryId, false);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async void CreateQuestionsCategory_ShouldCreateQuestionsCategory()
	{
		// Arrange
		var questionsCategory = new QuestionsCategory
		{
			Id = 3,
			ImpactOnAmountOfTime = 1,
			Name = ""
		};

		// Act
		_questionsCategoryRepository.CreateQuestionsCategory(questionsCategory);
		await _fixture.ApplicationContext.SaveChangesAsync();
		var result = await _questionsCategoryRepository.GetQuestionsCategoryAsync(3, false);

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public async void DeleteQuestionsCategory_ShouldRemoveQuestionsCategoryFromDatabase()
	{
		// Arrange
		var questionsCategory = await _questionsCategoryRepository.GetQuestionsCategoryAsync(4, true);

		// Act
		_questionsCategoryRepository.DeleteQuestionsCategory(questionsCategory);
		await _fixture.ApplicationContext.SaveChangesAsync();
		var result = await _fixture.ApplicationContext.QuestionsCategories.FindAsync(questionsCategory.Id);

		// Assert
		Assert.Null(result);
	}
}