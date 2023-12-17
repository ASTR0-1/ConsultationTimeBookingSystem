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
	private readonly UserManager<Student> _studentManager;
	private readonly UserManager<Lecturer> _lecturerManager;
	private readonly IAuthenticationManager _authManager;

	public AuthenticationController(IMapper mapper, UserManager<Student> studentManager, UserManager<Lecturer> lecturerManager, IAuthenticationManager authManager)
	{
		_mapper = mapper;
		_studentManager = studentManager;
		_lecturerManager = lecturerManager;
		_authManager = authManager;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
	{
		switch (userForRegistrationDto.UserType)
		{
			case UserType.Lecturer:
				{
					var lecturer = _mapper.Map<Lecturer>(userForRegistrationDto);
					var result = await _lecturerManager.CreateAsync(lecturer, userForRegistrationDto.Password);

					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
							ModelState.TryAddModelError(error.Code, error.Description);

						return BadRequest(ModelState);
					}

					await _lecturerManager.AddToRoleAsync(lecturer, userForRegistrationDto.UserType.ToString());

					break;
				}
			case UserType.Student:
				{
					var student = _mapper.Map<Student>(userForRegistrationDto);
					var result = await _studentManager.CreateAsync(student, userForRegistrationDto.Password);

					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
							ModelState.TryAddModelError(error.Code, error.Description);

						return BadRequest(ModelState);
					}

					await _studentManager.AddToRoleAsync(student, userForRegistrationDto.UserType.ToString());

					break;
				}
			default:
				{
					throw new KeyNotFoundException($"User type {userForRegistrationDto.UserType} not found.");
				}
		}

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