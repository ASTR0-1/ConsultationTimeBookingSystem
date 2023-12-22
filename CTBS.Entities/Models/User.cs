using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CTBS.Entities.Models;

public class User : IdentityUser<int>
{
	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }

	public ICollection<Appointment> Appointments { get; set; }
}
