using CTBS.Application.Interfaces;
using CTBS.Infrastructure.Persistence.Repositories;

namespace CTBS.Infrastructure.Persistence;

public class RepositoryManager : IRepositoryManager
{
	private readonly ApplicationContext _applicationContext;

	private IAppointmentRepository? _appointmentRepository;
	private IQuestionsCategoryRepository? _questionsCategoryRepository;
	private IUserRepository? _userRepository;

	public RepositoryManager(ApplicationContext applicationApplicationContext)
	{
		_applicationContext = applicationApplicationContext;
	}

	public IAppointmentRepository Appointment =>
		_appointmentRepository ??= new AppointmentRepository(_applicationContext);

	public IUserRepository User => _userRepository ??= new UserRepository(_applicationContext);

	public IQuestionsCategoryRepository QuestionsCategory =>
		_questionsCategoryRepository ??= new QuestionsCategoryRepository(_applicationContext);

	public Task SaveAsync()
	{
		return _applicationContext.SaveChangesAsync();
	}
}