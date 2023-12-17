namespace CTBS.Entities.DataTransferObjects.Appointment;

public class CreateAppointmentDto
{
	public int LecturerId { get; set; }
	public int StudentId { get; set; }
	public int QuestionsCategoryId { get; set; }
}
