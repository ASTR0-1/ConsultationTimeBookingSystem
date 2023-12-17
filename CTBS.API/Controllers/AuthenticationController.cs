using AutoMapper;
using CTBS.Contracts;
using CTBS.Entities.DataTransferObjects.Authentication;
using CTBS.Entities.Enums;
using CTBS.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/Authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly IAuthenticationManager _authManager;

	public AuthenticationController(IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager)
	{
		_mapper = mapper;
		_userManager = userManager;
		_authManager = authManager;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
	{
		var user = _mapper.Map<User>(userForRegistrationDto);
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

	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
	{
		if (!await _authManager.ValidateUserAsync(user))
			return Unauthorized();

		return Ok(new
		{
			Token = await _authManager.CreateTokenAsync()
		});
	}
}