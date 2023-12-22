using CTBS.Contracts;
using CTBS.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
	private readonly IRepositoryManager _repository;

	public UserController(IRepositoryManager repository)
	{
		_repository = repository;
	}

	/// <summary>
	/// Gets all users ordered by first and last name ascending.
	/// </summary>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated users ordered by first and last name ascending.</returns>
	/// <remarks>HTTP GET: api/users</remarks>
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var users = await _repository.User!
				.GetAllUsersAsync(requestParameters, false);

			return Ok(users);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Gets user by id.
	/// </summary>
	/// <param name="userId">User id to retrieve information.</param>
	/// <returns>User by provided id.</returns>
	/// <remarks>HTTP GET: api/users/{userId}</remarks>
	[HttpGet("{userId:int}")]
	public async Task<IActionResult> GetById(int userId)
	{
		try
		{
			var user = await _repository.User!.GetUserAsync(userId, true);
			if (user is null)
				return NotFound($"User with ID: {userId} not found.");

			return Ok(user);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Deletes user.
	/// </summary>
	/// <param name="userId">User id to delete.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP DELETE: api/users/{userId}</remarks>
	[HttpDelete("{userId:int}")]
	public async Task<IActionResult> DeleteById(int userId)
	{
		try
		{
			var userToDelete = await _repository.User!.GetUserAsync(userId, true);
			if (userToDelete is null)
				return NotFound($"User with ID: {userId} not found.");

			_repository.User!.DeleteUser(userToDelete);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}
}
