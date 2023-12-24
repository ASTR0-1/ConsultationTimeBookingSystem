using AutoMapper.Configuration.Annotations;

namespace CTBS.Application.DataTransferObjects.User;

public class GetUserDto
{
	public int Id { get; set; }

	public string FirstName { get; set; }
	public string MiddleName { get; set; }
	public string LastName { get; set; }

	public ICollection<Domain.Models.Appointment> Appointments { get; set; }

	[Ignore] public string? Role { get; set; }
}