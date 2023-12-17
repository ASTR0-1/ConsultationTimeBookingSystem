using CTBS.Contracts;
using CTBS.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/lecturers")]
[ApiController]
[Authorize]
public class LecturerController : ControllerBase
{
	private readonly IRepositoryManager _repository;

	public LecturerController(IRepositoryManager repository)
	{
		_repository = repository;
	}

	/// <summary>
	/// Gets all lecturers ordered by first and last name ascending.
	/// </summary>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated lecturers ordered by first and last name ascending.</returns>
	/// <remarks>HTTP GET: api/lecturers</remarks>
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var lecturers = await _repository.Lecturer!
				.GetAllLecturersAsync(requestParameters, false);

			return Ok(lecturers);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Gets lecturer by id.
	/// </summary>
	/// <param name="lecturerId">Lecturer id to retrieve information.</param>
	/// <returns>Lecturer by provided id.</returns>
	/// <remarks>HTTP GET: api/lecturers/{lecturerId}</remarks>
	[HttpGet("{lecturerId:int}")]
	public async Task<IActionResult> GetById(int lecturerId)
	{
		try
		{
			var lecturer = await _repository.Lecturer!.GetLecturerAsync(lecturerId, true);
			if (lecturer is null)
				return NotFound($"Lecturer with ID: {lecturerId} not found.");

			return Ok(lecturer);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Deletes lecturer.
	/// </summary>
	/// <param name="lecturerId">Lecturer id to delete.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP DELETE: api/lecturers/{lecturerId}</remarks>
	[HttpDelete("{lecturerId:int}")]
	public async Task<IActionResult> DeleteById(int lecturerId)
	{
		try
		{
			var lecturerToDelete = await _repository.Lecturer!.GetLecturerAsync(lecturerId, true);
			if (lecturerToDelete is null)
				return NotFound($"Lecturer with ID: {lecturerId} not found.");

			_repository.Lecturer!.DeleteLecturer(lecturerToDelete);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}
}
