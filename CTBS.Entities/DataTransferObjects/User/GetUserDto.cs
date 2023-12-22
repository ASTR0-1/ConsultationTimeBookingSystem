using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace CTBS.Entities.DataTransferObjects.User;
internal class GetUserDto
{
	[Ignore]
	public int Id { get; set; }

	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }

	public ICollection<Models.Appointment> Appointments { get; set; }

	[Ignore]
	public string Role { get; set; }
}
