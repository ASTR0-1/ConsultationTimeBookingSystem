using System.Net.Http.Headers;
using CTBS.Application.RequestFeatures;
using CTBS.Tests.IntegrationTests.TestConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CTBS.Tests.IntegrationTests;

public class UserControllerTests : IClassFixture<ApplicationFactory>
{
	private readonly HttpClient _client;

	public UserControllerTests(ApplicationFactory factory)
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
		var requestParameters = new RequestParameters {PageSize = 10, PageNumber = 1};

		// Act
		var response =
			await _client.GetAsync(
				$"/api/users?pageSize={requestParameters.PageSize}&pageNumber={requestParameters.PageNumber}");

		// Assert
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task GetById_ReturnsSuccessStatusCode()
	{
		// Arrange
		var id = 2;

		// Act
		var response = await _client.GetAsync($"/api/users/{id}");

		// Assert
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task DeleteById_ReturnsSuccessStatusCode()
	{
		// Arrange
		var id = 1;

		// Act
		var response = await _client.DeleteAsync($"/api/users/{id}");

		// Assert
		response.EnsureSuccessStatusCode();
	}
}