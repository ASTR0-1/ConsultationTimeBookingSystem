using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CTBS.Contracts;
using CTBS.Entities.DataTransferObjects.Appointment;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/Appointments")]
[ApiController]
[Authorize]
public class AppointmentController : ControllerBase
{
	private readonly IRepositoryManager _repository;
	private readonly IMapper _mapper;

	public AppointmentController(IRepositoryManager repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Gets student appointments in an ascending order.
	/// </summary>
	/// <param name="studentId">The Student Id to search for appointments.</param>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated appointments for student in ascending order.</returns>
	/// <remarks>HTTP GET: api/Appointments/Student/{studentId}</remarks>
	[HttpGet("Student/{studentId:int}")]
	public async Task<IActionResult> GetStudentAppointments([Required] int studentId,
		[FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var studentAppointments = await _repository.Appointment!
				.GetStudentAppointmentAsync(studentId, requestParameters, false);

			return Ok(studentAppointments);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Gets lecturer appointments in an ascending order.
	/// </summary>
	/// <param name="lecturerId">The Student Id to search for appointments.</param>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated appointments for student in ascending order.</returns>
	/// <remarks>HTTP GET: api/Appointments/Lecturer/{lecturerId}</remarks>
	[HttpGet("Lecturer/{lecturerId:int}")]
	[Authorize(Roles = "Lecturer")]
	public async Task<IActionResult> GetLecturerAppointments([Required] int lecturerId,
		[FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var lecturerAppointments = await _repository.Appointment!
				.GetLecturerAppointmentsAsync(lecturerId, requestParameters, false);

			return Ok(lecturerAppointments);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Creates appointment from provided appointment data transfer object.
	/// </summary>
	/// <param name="appointmentDto">Data transfer object to create an appointment.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP POST: api/Appointments</remarks>
	[HttpPost]
	[Authorize(Roles = "Student")]
	public async Task<IActionResult> CreateAppointment(CreateAppointmentDto appointmentDto)
	{
		try
		{
			var appointment = _mapper.Map<Appointment>(appointmentDto);

			_repository.Appointment!.CreateAppointment(appointment);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Updates an appointment state.
	/// </summary>
	/// <param name="appointmentId">An appointment id to update.</param>
	/// <param name="appointmentDto">An appointment data transfer object with appointment state.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP PATCH: api/Appointments/{appointmentId}</remarks>
	[HttpPatch("{appointmentId:int}")]
	public async Task<IActionResult> UpdateAppointmentState(int appointmentId, UpdateAppointmentDto appointmentDto)
	{
		var appointmentToPatch = await _repository.Appointment!.GetAppointmentAsync(appointmentId, true);

		if (appointmentToPatch is null)
			return NotFound($"Appointment with ID: {appointmentId} not found.");

		appointmentToPatch.State = appointmentDto.State;
		await _repository.SaveAsync();

		return NoContent();
	}
}
