using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTBS.Contracts;

public interface IRepositoryManager
{
	IAppointmentRepository? Appointment { get; }
	IUserRepository? User { get; }
	IQuestionsCategoryRepository? QuestionsCategory { get; }

	Task SaveAsync();
}
