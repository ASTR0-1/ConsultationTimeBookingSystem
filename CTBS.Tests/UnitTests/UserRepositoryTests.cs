using CTBS.Contracts;
using CTBS.Entities.RequestFeatures;
using CTBS.Repository;
using CTBS.Tests.UnitTests.TestFixtures;
using Xunit;

namespace CTBS.Tests.UnitTests;

public class UserRepositoryTests : IClassFixture<UserRepositorySeedDataFixture>
{
	private readonly UserRepositorySeedDataFixture _fixture;
	private readonly IUserRepository _userRepository;

	public UserRepositoryTests(UserRepositorySeedDataFixture fixture)
	{
		_fixture = fixture;
		_userRepository = new UserRepository(_fixture.ApplicationContext);
	}

	[Fact]
	public async void GetUserAsync_ShouldReturnUser_WhenUserExists()
	{
		// Arrange
		var userId = 1;

		// Act
		var result = await _userRepository.GetUserAsync(userId, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(userId, result.Id);
	}

	[Fact]
	public async void GetUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
	{
		// Arrange
		var userId = 999;

		// Act
		var result = await _userRepository.GetUserAsync(userId, false);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async void GetAllUsersAsync_ShouldReturnAllUsers()
	{
		// Arrange
		var requestParameters = new RequestParameters {PageNumber = 1, PageSize = 10};

		// Act
		var result = await _userRepository.GetAllUsersAsync(requestParameters, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Count);
	}

	[Fact]
	public async void DeleteUser_ShouldRemoveUserFromDatabase()
	{
		// Arrange
		var user = await _userRepository.GetUserAsync(300, true);
		_userRepository.DeleteUser(user);
		await _fixture.ApplicationContext.SaveChangesAsync();

		// Act
		var result = await _fixture.ApplicationContext.Users.FindAsync(user.Id);

		// Assert
		Assert.Null(result);
	}
}