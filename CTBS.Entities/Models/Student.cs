using Microsoft.AspNetCore.Identity;

namespace CTBS.Entities.Models;

public class Student : User
{
	public ICollection<Appointment> Appointments { get; set; }
}
