using CTBS.Contracts;
using CTBS.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/Students")]
[ApiController]
[Authorize]
public class StudentController : ControllerBase
{
	private readonly IRepositoryManager _repository;

	public StudentController(IRepositoryManager repository)
	{
		_repository = repository;
	}

	/// <summary>
	/// Gets all students ordered by first and last name ascending.
	/// </summary>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated students ordered by first and last name ascending.</returns>
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var students = await _repository.Student!
				.GetAllStudentsAsync(requestParameters, false);

			return Ok(students);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Gets student by id.
	/// </summary>
	/// <param name="studentId">Student id to retrieve information.</param>
	/// <returns>Student by provided id.</returns>
	/// <remarks>HTTP GET: api/Students/{studentId}</remarks>
	[HttpGet("{studentId:int}")]
	public async Task<IActionResult> GetById(int studentId)
	{
		try
		{
			var student = await _repository.Student!.GetStudentAsync(studentId, true);
			if (student is null)
				return NotFound($"Student with ID: {studentId} not found.");

			return Ok(student);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Deletes student.
	/// </summary>
	/// <param name="studentId">Student id to delete.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP DELETE: api/Students/{studentId}</remarks>
	[HttpDelete("{studentId:int}")]
	public async Task<IActionResult> DeleteById(int studentId)
	{
		try
		{
			var studentToDelete = await _repository.Student!.GetStudentAsync(studentId, true);
			if (studentToDelete is null)
				return NotFound($"Student with ID: {studentId} not found.");

			_repository.Student!.DeleteStudent(studentToDelete);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}
}
