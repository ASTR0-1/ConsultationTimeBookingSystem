using Microsoft.AspNetCore.Identity;

namespace CTBS.Entities.Models;

public class Lecturer : IdentityUser
{
	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }

	public Guid SubjectId { get; set; }
	public Subject Subject { get; set; }

	public ICollection<Appointment> Appointments { get; set; }
}
