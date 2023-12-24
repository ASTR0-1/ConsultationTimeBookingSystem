using CTBS.Application.RequestFeatures;
using CTBS.Domain.Models;

namespace CTBS.Application.Interfaces;

public interface IAppointmentRepository
{
	Task<Appointment?> GetAppointmentAsync(int appointmentId, bool trackChanges);

	Task<PagedList<Appointment>> GetLecturerAppointmentsAsync(int lecturerId, RequestParameters requestParameters,
		bool trackChanges);

	Task<PagedList<Appointment>> GetStudentAppointmentAsync(int studentId, RequestParameters requestParameters,
		bool trackChanges);

	void CreateAppointment(Appointment appointment);
	void UpdateAppointment(Appointment appointment);
}