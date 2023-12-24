using CTBS.Domain.Enums;

namespace CTBS.Application.DataTransferObjects.Appointment;

public class UpdateAppointmentDto
{
	public AppointmentState State { get; set; }
}