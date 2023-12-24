using AutoMapper;
using CTBS.Application.DataTransferObjects.User;
using CTBS.Application.Interfaces;
using CTBS.Application.RequestFeatures;
using CTBS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IRepositoryManager _repository;
	private readonly UserManager<User> _userManager;

	public UserController(IRepositoryManager repository, UserManager<User> userManager, IMapper mapper)
	{
		_repository = repository;
		_userManager = userManager;
		_mapper = mapper;
	}

	/// <summary>
	///     Gets all users ordered by first and last name ascending.
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
			var mappedUsers = _mapper.Map<List<User>, List<GetUserDto>>(users);

			foreach (var mappedUser in mappedUsers)
				mappedUser.Role =
					(await _userManager.GetRolesAsync(users?.FirstOrDefault(u => u?.Id == mappedUser?.Id)))
					?.FirstOrDefault();

			return Ok(mappedUsers);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	///     Gets user by id.
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

			var mappedUser = _mapper.Map<User, GetUserDto>(user);
			mappedUser.Role = (await _userManager.GetRolesAsync(user))
				?.FirstOrDefault();

			return Ok(mappedUser);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	///     Deletes user.
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