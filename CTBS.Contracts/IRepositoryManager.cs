using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTBS.Contracts;

public interface IRepositoryManager
{
	IAppointmentRepository? Appointment { get; }
	ILecturerRepository? Lecturer { get; }
	IQuestionsCategoryRepository? QuestionsCategory { get; }
	IStudentRepository? Student { get; }
	ISubjectRepository? Subject { get; }

	Task SaveAsync();
}
