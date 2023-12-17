using AutoMapper;
using CTBS.Contracts;
using CTBS.Entities.DataTransferObjects.Subject;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/Subjects")]
[ApiController]
[Authorize(Roles = "Lecturer")]
public class SubjectController : ControllerBase
{
	private readonly IRepositoryManager _repository;
	private readonly IMapper _mapper;

	public SubjectController(IRepositoryManager repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Gets all subjects ordered by id ascending.
	/// </summary>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated subjects ordered by name ascending.</returns>
	/// <remarks>HTTP GET: api/Subjects</remarks>
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var subject = await _repository.Subject!
				.GetAllSubjectsAsync(requestParameters, false);

			return Ok(subject);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Creates subject from provided subject data transfer object.
	/// </summary>
	/// <param name="subjectDto">Data transfer object to create the subject.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP POST: api/Subjects</remarks>
	[HttpPost]
	public async Task<IActionResult> CreateSubject(CreateSubjectDto subjectDto)
	{
		try
		{
			var subject = _mapper.Map<Subject>(subjectDto);

			_repository.Subject!.CreateSubject(subject);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Deletes subject by provided id.
	/// </summary>
	/// <param name="subjectId">The subject id to delete.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP DELETE: api/Subjects/{subjectId}</remarks>
	[HttpDelete("{subjectId:int}")]
	public async Task<IActionResult> DeleteSubject(int subjectId)
	{
		try
		{
			var subjectToDelete = await _repository.Subject!.GetSubjectAsync(subjectId, true);

			if (subjectToDelete is null)
				return NotFound($"Subject with ID: {subjectId} not found.");

			_repository.Subject!.DeleteSubject(subjectToDelete);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}
}
