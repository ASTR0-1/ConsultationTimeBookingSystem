using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CTBS.Application.DataTransferObjects.Appointment;
using CTBS.Domain.Enums;
using CTBS.Tests.IntegrationTests.TestConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace CTBS.Tests.IntegrationTests;

public class AppointmentControllerTests : IClassFixture<ApplicationFactory>
{
	private readonly HttpClient _client;

	public AppointmentControllerTests(ApplicationFactory factory)
	{
		_client = factory.CreateClient();

		var configuration = factory.Services.GetRequiredService<IConfiguration>();
		var token = TestAuthHandler.CreateToken(configuration);
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	}

	[Fact]
	public async Task GetStudentAppointments_ReturnsSuccessStatusCode()
	{
		// Act
		var response = await _client.GetAsync("/api/appointments/student/1");

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Fact]
	public async Task GetLecturerAppointments_ReturnsSuccessStatusCode()
	{
		// Act
		var response = await _client.GetAsync("/api/appointments/lecturer/1");

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Fact]
	public async Task CreateAppointment_ReturnsNoContentStatusCode()
	{
		// Arrange
		var appointmentDto = new CreateAppointmentDto
		{
			Date = new DateTime(2023, 11, 11),
			QuestionsCategoryId = 2,
			LecturerId = 1,
			StudentId = 2,
			RequestedMinutes = 60
		};
		var content = new StringContent(JsonConvert.SerializeObject(appointmentDto), Encoding.UTF8, "application/json");

		// Act
		var response = await _client.PostAsync("/api/appointments", content);

		// Assert
		Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
	}

	[Fact]
	public async Task UpdateAppointmentState_ReturnsNoContentStatusCode()
	{
		// Arrange
		var appointmentDto = new UpdateAppointmentDto
		{
			State = AppointmentState.Visited
		};
		var content = new StringContent(JsonConvert.SerializeObject(appointmentDto), Encoding.UTF8, "application/json");

		// Act
		var response = await _client.PatchAsync("/api/appointments/1", content);

		// Assert
		Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
	}
}