using AutoMapper;
using CTBS.Contracts;
using CTBS.Entities.DataTransferObjects.QuestionsCategory;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTBS.API.Controllers;

[Route("api/questionsCategories")]
[ApiController]
[Authorize]
public class QuestionsCategoryController : Controller
{
	private readonly IRepositoryManager _repository;
	private readonly IMapper _mapper;

	public QuestionsCategoryController(IRepositoryManager repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Gets all question categories ordered by id ascending.
	/// </summary>
	/// <param name="requestParameters">The request parameters to apply pagination.</param>
	/// <returns>Paginated questions categories ordered by name ascending.</returns>
	/// <remarks>HTTP GET: api/questionsCategories</remarks>
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
	{
		try
		{
			var questionsCategory = await _repository.QuestionsCategory!
				.GetAllQuestionsCategoriesAsync(requestParameters, false);

			return Ok(questionsCategory);
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Creates questions category from provided questions category data transfer object.
	/// </summary>
	/// <param name="questionsCategoryDto">Data transfer object to create the questions category.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP POST: api/questionsCategories</remarks>
	[HttpPost]
	public async Task<IActionResult> CreateQuestionsCategory(CreateQuestionsCategoryDto questionsCategoryDto)
	{
		try
		{
			var questionsCategory = _mapper.Map<QuestionsCategory>(questionsCategoryDto);

			_repository.QuestionsCategory!.CreateQuestionsCategory(questionsCategory);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}

	/// <summary>
	/// Deletes questions category by provided id.
	/// </summary>
	/// <param name="questionsCategoryId">The questions category id to delete.</param>
	/// <returns>No content result.</returns>
	/// <remarks>HTTP DELETE: api/questionsCategories/{questionsCategoryId}</remarks>
	[HttpDelete("{questionsCategoryId:int}")]
	public async Task<IActionResult> DeleteQuestionsCategory(int questionsCategoryId)
	{
		try
		{
			var questionsCategoryToDelete = await _repository.QuestionsCategory!.GetQuestionsCategoryAsync(questionsCategoryId, true);

			if (questionsCategoryToDelete is null)
				return NotFound($"Questions category with ID: {questionsCategoryId} not found.");

			_repository.QuestionsCategory.DeleteQuestionsCategory(questionsCategoryToDelete);
			await _repository.SaveAsync();

			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest();
		}
	}
}
