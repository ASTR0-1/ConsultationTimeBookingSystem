using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CTBS.Entities.DataTransferObjects.QuestionsCategory;
using CTBS.Entities.RequestFeatures;
using CTBS.Tests.IntegrationTests.TestConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CTBS.Tests.IntegrationTests;

public class QuestionsCategoryTests : IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;

    public QuestionsCategoryTests(ApplicationFactory factory)
    {
		_client = factory.CreateClient();

		var configuration = factory.Services.GetRequiredService<IConfiguration>();
		var token = TestAuthHandler.CreateToken(configuration);
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	}

    [Fact]
    public async Task GetAll_ReturnsSuccessStatusCode()
    {
	    // Arrange
	    var requestParameters = new RequestParameters { PageSize = 10, PageNumber = 1 };

	    // Act
	    var response = await _client.GetAsync($"/api/questionsCategories?pageSize={requestParameters.PageSize}&pageNumber={requestParameters.PageNumber}");

	    // Assert
	    response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById_ReturnsSuccessStatusCode()
    {
	    // Arrange
	    var id = 1;

	    // Act
	    var response = await _client.GetAsync($"/api/questionsCategories/{id}");

	    // Assert
	    response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateQuestionsCategory_ReturnsSuccessStatusCode()
    {
	    // Arrange
	    var questionsCategoryDto = new CreateQuestionsCategoryDto
	    {
			Name = "",
			ImpactOnAmountOfTime = 1
	    };

	    // Act
	    var response = await _client.PostAsJsonAsync("/api/questionsCategories", questionsCategoryDto);

	    // Assert
	    response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteQuestionsCategory_ReturnsSuccessStatusCode()
    {
	    // Arrange
	    var id = 1;

	    // Act
	    var response = await _client.DeleteAsync($"/api/questionsCategories/{id}");

	    // Assert
	    response.EnsureSuccessStatusCode();
    }
}