using CTBS.Contracts;
using CTBS.Entities;

namespace CTBS.Repository;

public class RepositoryManager : IRepositoryManager
{
	private readonly ApplicationContext _applicationContext;

	private IAppointmentRepository? _appointmentRepository;
	private IUserRepository? _userRepository;
	private IQuestionsCategoryRepository? _questionsCategoryRepository;

	public RepositoryManager(ApplicationContext applicationApplicationContext)
	{
		_applicationContext = applicationApplicationContext;
	}

	public IAppointmentRepository Appointment => _appointmentRepository ??= new AppointmentRepository(_applicationContext);
	public IUserRepository User => _userRepository ??= new UserRepository(_applicationContext);
	public IQuestionsCategoryRepository QuestionsCategory => _questionsCategoryRepository ??= new QuestionsCategoryRepository(_applicationContext);

	public Task SaveAsync() => _applicationContext.SaveChangesAsync();
}
