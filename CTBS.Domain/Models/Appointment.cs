using CTBS.Domain.Enums;

namespace CTBS.Domain.Models;

public class Appointment
{
	public int Id { get; set; }
	public float Priority { get; set; }
	public float RequestedMinutes { get; set; }
	public DateOnly Date { get; set; }
	public AppointmentState State { get; set; }

	public int? LecturerId { get; set; }
	public User? Lecturer { get; set; }

	public int? StudentId { get; set; }
	public User? Student { get; set; }

	public int? QuestionsCategoryId { get; set; }
	public QuestionsCategory? QuestionsCategory { get; set; }
}