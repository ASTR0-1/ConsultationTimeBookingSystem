using CTBS.Application.Interfaces;
using CTBS.Application.RequestFeatures;
using CTBS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Infrastructure.Persistence.Repositories;

public class QuestionsCategoryRepository : RepositoryBase<QuestionsCategory>, IQuestionsCategoryRepository
{
	public QuestionsCategoryRepository(ApplicationContext applicationContext)
		: base(applicationContext)
	{
	}

	public async Task<PagedList<QuestionsCategory>> GetAllQuestionsCategoriesAsync(RequestParameters requestParameters,
		bool trackChanges)
	{
		return PagedList<QuestionsCategory>.ToPagedList(await FindAll(trackChanges)
				.OrderBy(qc => qc.Name)
				.ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);
	}

	public async Task<QuestionsCategory?> GetQuestionsCategoryAsync(int questionsCategoryId, bool trackChanges)
	{
		return await FindByCondition(qc => qc.Id.Equals(questionsCategoryId), trackChanges)
			.SingleOrDefaultAsync();
	}

	public void CreateQuestionsCategory(QuestionsCategory questionsCategory)
	{
		Create(questionsCategory);
	}

	public void DeleteQuestionsCategory(QuestionsCategory questionsCategory)
	{
		Delete(questionsCategory);
	}
}