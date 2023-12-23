using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTBS.Contracts;
using CTBS.Entities;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Repository;

public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
{
	public AppointmentRepository(ApplicationContext applicationContext) 
		: base(applicationContext)
	{
	}

	public async Task<Appointment?> GetAppointmentAsync(int appointmentId, bool trackChanges) =>
		await FindByCondition(a => a.Id.Equals(appointmentId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<PagedList<Appointment>> GetLecturerAppointmentsAsync(int lecturerId, RequestParameters requestParameters, bool trackChanges) =>
		PagedList<Appointment>.ToPagedList(await FindByCondition(a => a.LecturerId.Equals(lecturerId), trackChanges)
				.OrderBy(a => a.Priority)
				.ThenByDescending(a => a.Date)
				.ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public async Task<PagedList<Appointment>> GetStudentAppointmentAsync(int studentId, RequestParameters requestParameters, bool trackChanges) =>
		PagedList<Appointment>.ToPagedList(await FindByCondition(a => a.StudentId.Equals(studentId), trackChanges)
				.OrderBy(a => a.Priority)
				.ThenByDescending(a => a.Date)
				.ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public void CreateAppointment(Appointment appointment) => Create(appointment);
	public void UpdateAppointment(Appointment appointment) => Update(appointment);
}
