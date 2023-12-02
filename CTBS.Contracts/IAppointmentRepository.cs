using CTBS.Entities.Enums;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;

namespace CTBS.Contracts;

public interface IAppointmentRepository
{
	Task<PagedList<Appointment>> GetLecturerAppointmentsAsync(int lecturerId, RequestParameters requestParameters,
		bool trackChanges);
	Task<PagedList<Appointment>> GetStudentAppointmentAsync(int studentId, RequestParameters requestParameters,
		bool trackChanges);
	void CreateAppointment(Appointment appointment);
	void UpdateAppointment(Appointment appointment);
}