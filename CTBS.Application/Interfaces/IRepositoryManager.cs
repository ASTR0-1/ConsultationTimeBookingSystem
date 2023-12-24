namespace CTBS.Application.Interfaces;

public interface IRepositoryManager
{
	IAppointmentRepository? Appointment { get; }
	IUserRepository? User { get; }
	IQuestionsCategoryRepository? QuestionsCategory { get; }

	Task SaveAsync();
}