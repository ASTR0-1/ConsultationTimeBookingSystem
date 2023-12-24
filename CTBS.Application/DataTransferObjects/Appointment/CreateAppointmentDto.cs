namespace CTBS.Application.DataTransferObjects.Appointment;

public class CreateAppointmentDto
{
	public DateTime Date { get; set; }
	public float RequestedMinutes { get; set; }
	public int LecturerId { get; set; }
	public int StudentId { get; set; }
	public int QuestionsCategoryId { get; set; }
}