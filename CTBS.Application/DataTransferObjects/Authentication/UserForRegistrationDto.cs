using System.ComponentModel.DataAnnotations;
using CTBS.Domain.Enums;

namespace CTBS.Application.DataTransferObjects.Authentication;

public class UserForRegistrationDto
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string MiddleName { get; set; }

	[Required(ErrorMessage = "Email is required")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Password is required")]
	public string Password { get; set; }

	/// <summary>
	///     Gets or sets the type of the user.
	///     Values: 0 (Lecturer), 1 (Student)
	/// </summary>
	/// <remarks>Specify whether the user is a Lecturer or a Student.</remarks>
	[Required(ErrorMessage = "User type is required")]
	public UserType UserType { get; set; }
}