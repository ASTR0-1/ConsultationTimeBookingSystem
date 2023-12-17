using Microsoft.AspNetCore.Identity;

namespace CTBS.Entities.Models;

public class Lecturer : User
{
	public int SubjectId { get; set; }
	public Subject Subject { get; set; }

	public ICollection<Appointment> Appointments { get; set; }
}
