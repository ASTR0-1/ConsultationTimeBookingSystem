﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CTBS.Application.DataTransferObjects.Appointment;
using CTBS.Application.Interfaces;
using CTBS.Application.RequestFeatures;
using CTBS.Domain.Enums;
using CTBS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/appointments")]
[ApiController]
[Authorize]
public class AppointmentController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IRepositoryManager _repository;

	public AppointmentController(IRepositoryManager repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	///     Gets student appointments in an ascending order.
	/// </summary>
	/// <param name="studentId">The Student Id to search for appointments.</param>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated appointments for student in an ascending order.</returns>
	/// <remarks>HTTP GET: api/appointments/student/{studentId}</remarks>
	[HttpGet("student/{studentId:int}")]
	public async Task<IActionResult> GetStudentAppointments([Required] int studentId,
		[FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var studentAppointments = await _repository.Appointment!
				.GetStudentAppointmentAsync(studentId, requestParameters, false);
			var appointmentDtos = _mapper.Map<PagedList<GetAppointmentDto>>(studentAppointments);

			return Ok(new {appointments = appointmentDtos, studentAppointments.MetaData});
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	///     Gets lecturer appointments in an ascending order.
	/// </summary>
	/// <param name="lecturerId">The Lecturer Id to search for appointments.</param>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated appointments for lecturer in an ascending order.</returns>
	/// <remarks>HTTP GET: api/appointments/lecturer/{lecturerId}</remarks>
	[HttpGet("lecturer/{lecturerId:int}")]
	[Authorize(Roles = "Lecturer")]
	public async Task<IActionResult> GetLecturerAppointments([Required] int lecturerId,
		[FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var lecturerAppointments = await _repository.Appointment!
				.GetLecturerAppointmentsAsync(lecturerId, requestParameters, false);
			var appointmentDtos = _mapper.Map<PagedList<GetAppointmentDto>>(lecturerAppointments);

			return Ok(new {appointments = appointmentDtos, lecturerAppointments.MetaData});
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	///     Creates appointment from provided appointment data transfer object.
	/// </summary>
	/// <param name="appointmentDto">Data transfer object to create an appointment.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP POST: api/appointments</remarks>
	[HttpPost]
	[Authorize(Roles = "Student")]
	public async Task<IActionResult> CreateAppointment(CreateAppointmentDto appointmentDto)
	{
		try
		{
			var appointment = _mapper.Map<Appointment>(appointmentDto);

			var questionsCategory = await _repository.QuestionsCategory
				!.GetQuestionsCategoryAsync(appointmentDto.QuestionsCategoryId, false);
			if (questionsCategory is null)
				return BadRequest(
					$"Questions category with provided ID: {appointmentDto.QuestionsCategoryId} not found.");
			appointment.Priority = questionsCategory.ImpactOnAmountOfTime * appointmentDto.RequestedMinutes;

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
	///     Updates an appointment state.
	/// </summary>
	/// <param name="appointmentId">An appointment id to update.</param>
	/// <param name="appointmentDto">An appointment data transfer object with appointment state.</param>
	/// <returns>No content result.</returns>
	/// <remarks>
	///     HTTP PATCH: api/appointments/{appointmentId}
	///     Present statuses:
	///     Pending = 0,
	///     Visited = 1,
	///     CanceledByStudent = 2,
	///     CanceledByLecturer = 3,
	///     Skipped = 4
	/// </remarks>
	[HttpPatch("{appointmentId:int}")]
	public async Task<IActionResult> UpdateAppointmentState(int appointmentId, UpdateAppointmentDto appointmentDto)
	{
		var appointmentToPatch = await _repository.Appointment!.GetAppointmentAsync(appointmentId, true);

		if (appointmentToPatch is null)
			return NotFound($"Appointment with ID: {appointmentId} not found.");

		appointmentToPatch.State = appointmentDto.State;
		if (appointmentDto.State == AppointmentState.Skipped)
			appointmentToPatch.Priority *= (int) AppointmentState.Skipped;

		await _repository.SaveAsync();

		return NoContent();
	}
}