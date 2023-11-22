using CTBS.Entities.Enums;

namespace CTBS.Entities.Models;

public class Appointment
{
	public Guid Id { get; set; }
	public float Priority { get; set; }
	public DateOnly Date { get; set; }
	public AppointmentState State { get; set; }

	public Guid LecturerId { get; set; }
	public Lecturer Lecturer { get; set; }

	public Guid StudentId { get; set; }
	public Student Student { get; set; }

	public Guid QuestionsCategoryId { get; set; }
	public QuestionsCategory QuestionsCategory { get; set; }
}