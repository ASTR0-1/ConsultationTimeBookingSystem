using AutoMapper;
using CTBS.Application.DataTransferObjects.Authentication;
using CTBS.Application.Interfaces;
using CTBS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly IAuthenticationManager _authManager;
	private readonly IMapper _mapper;
	private readonly UserManager<User> _userManager;

	public AuthenticationController(IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager)
	{
		_mapper = mapper;
		_userManager = userManager;
		_authManager = authManager;
	}

	/// <summary>
	///     Registers userForAuthenticationDto from provided userForAuthenticationDto data transfer object.
	/// </summary>
	/// <param name="userForRegistrationDto">Data transfer object to create the userForAuthenticationDto</param>
	/// <returns>Created result.</returns>
	/// <remarks>HTTP POST: api/authentication/register</remarks>
	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
	{
		var user = _mapper.Map<User>(userForRegistrationDto);
		user.UserName = userForRegistrationDto.Email;
		var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

		if (!result.Succeeded)
		{
			foreach (var error in result.Errors)
				ModelState.TryAddModelError(error.Code, error.Description);

			return BadRequest(ModelState);
		}

		await _userManager.AddToRoleAsync(user, userForRegistrationDto.UserType.ToString());

		return StatusCode(201);
	}

	/// <summary>
	///     Sign up userForAuthenticationDto by provided credentials
	/// </summary>
	/// <param name="userForAuthenticationDto">Data transfer object for sign up.</param>
	/// <returns>Ok result.</returns>
	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
	{
		if (!await _authManager.ValidateUserAsync(userForAuthenticationDto))
			return Unauthorized();

		var user = await _userManager.FindByEmailAsync(userForAuthenticationDto.Email);

		return Ok(new
		{
			UserId = user.Id,
			Token = await _authManager.CreateTokenAsync()
		});
	}
}