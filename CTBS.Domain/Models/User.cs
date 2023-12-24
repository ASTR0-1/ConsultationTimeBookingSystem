using Microsoft.AspNetCore.Identity;

namespace CTBS.Domain.Models;

public class User : IdentityUser<int>
{
	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }

	public ICollection<Appointment> Appointments { get; set; }
}