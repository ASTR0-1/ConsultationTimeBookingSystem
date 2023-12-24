namespace CTBS.Domain.Models;

public class QuestionsCategory
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ImpactOnAmountOfTime { get; set; }

	public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}