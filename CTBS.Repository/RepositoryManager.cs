using CTBS.Contracts;
using CTBS.Entities;

namespace CTBS.Repository;

public class RepositoryManager : IRepositoryManager
{
	private readonly ApplicationContext _applicationContext;

	private IAppointmentRepository? _appointmentRepository;
	private ILecturerRepository? _lecturerRepository;
	private IQuestionsCategoryRepository? _questionsCategoryRepository;
	private IStudentRepository? _studentRepository;
	private ISubjectRepository? _subjectRepository;

	public RepositoryManager(ApplicationContext applicationApplicationContext)
	{
		_applicationContext = applicationApplicationContext;
	}

	public IAppointmentRepository Appointment => _appointmentRepository ??= new AppointmentRepository(_applicationContext);
	public ILecturerRepository Lecturer => _lecturerRepository ??= new LecturerRepository(_applicationContext);
	public IQuestionsCategoryRepository QuestionsCategory => _questionsCategoryRepository ??= new QuestionsCategoryRepository(_applicationContext);
	public IStudentRepository Student => _studentRepository ??= new StudentRepository(_applicationContext);
	public ISubjectRepository Subject => _subjectRepository ??= new SubjectRepository(_applicationContext);

	public Task SaveAsync() => _applicationContext.SaveChangesAsync();
}
