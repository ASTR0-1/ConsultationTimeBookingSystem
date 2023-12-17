using CTBS.Entities.Enums;

namespace CTBS.Entities.DataTransferObjects.Appointment;

public class UpdateAppointmentDto
{
	public AppointmentState State { get; set; }
}
