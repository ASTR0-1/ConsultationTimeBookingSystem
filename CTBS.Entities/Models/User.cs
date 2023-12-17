using Microsoft.AspNetCore.Identity;

namespace CTBS.Entities.Models;

public class User : IdentityUser<int>
{
	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }
}
