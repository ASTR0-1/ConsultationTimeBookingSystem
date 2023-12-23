using CTBS.Contracts;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using CTBS.Repository;
using CTBS.Tests.UnitTests.TestFixtures;
using Xunit;

namespace CTBS.Tests.UnitTests;

public class AppointmentRepositoryTests : IClassFixture<AppointmentRepositorySeedDataFixture>
{
	private readonly IAppointmentRepository _appointmentRepository;
	private readonly AppointmentRepositorySeedDataFixture _fixture;

	public AppointmentRepositoryTests(AppointmentRepositorySeedDataFixture fixture)
	{
		_fixture = fixture;
		_appointmentRepository = new AppointmentRepository(_fixture.ApplicationContext);
	}

	[Fact]
	public async void GetAppointmentAsync_ShouldReturnAppointment_WhenAppointmentExists()
	{
		// Arrange
		var appointmentId = 1;

		// Act
		var result = await _appointmentRepository.GetAppointmentAsync(appointmentId, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(appointmentId, result.Id);
	}

	[Fact]
	public async void GetAppointmentAsync_ShouldReturnNull_WhenAppointmentDoesNotExist()
	{
		// Arrange
		var appointmentId = 999;

		// Act
		var result = await _appointmentRepository.GetAppointmentAsync(appointmentId, false);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async void GetLecturerAppointmentsAsync_ShouldReturnAllLecturerAppointments()
	{
		// Arrange
		var requestParameters = new RequestParameters {PageNumber = 1, PageSize = 10};
		var lecturerId = 1;

		// Act
		var result = await _appointmentRepository.GetLecturerAppointmentsAsync(lecturerId, requestParameters, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Count);
	}

	[Fact]
	public async void GetStudentAppointmentAsync_ShouldReturnAllStudentAppointments()
	{
		// Arrange
		var requestParameters = new RequestParameters {PageNumber = 1, PageSize = 10};
		var studentId = 1;

		// Act
		var result = await _appointmentRepository.GetStudentAppointmentAsync(studentId, requestParameters, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Count);
	}

	[Fact]
	public async void CreateAppointment_ShouldCreateAppointmentInDatabase()
	{
		// Arrange
		var appointment = new Appointment
		{
			Id = 300,
			Date = new DateOnly(1000, 1, 1),
			Priority = 0,
			RequestedMinutes = 60
		};

		// Act
		_appointmentRepository.CreateAppointment(appointment);
		await _fixture.ApplicationContext.SaveChangesAsync();
		var result = await _appointmentRepository.GetAppointmentAsync(300, false);

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public async void UpdateAppointment_ShouldUpdateAppointmentInDatabase()
	{
		// Arrange
		var appointment = await _appointmentRepository.GetAppointmentAsync(1, true);

		// Act
		appointment.Priority = 3;
		_appointmentRepository.UpdateAppointment(appointment);
		await _fixture.ApplicationContext.SaveChangesAsync();
		var result = await _fixture.ApplicationContext.Appointments.FindAsync(appointment.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Priority);
	}
}