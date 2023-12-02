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

public class QuestionsCategoryRepository : RepositoryBase<QuestionsCategory>, IQuestionsCategoryRepository
{
	public QuestionsCategoryRepository(ApplicationContext applicationContext) 
		: base(applicationContext)
	{
	}

	public async Task<PagedList<QuestionsCategory>> GetAllQuestionsCategoriesAsync(RequestParameters requestParameters, bool trackChanges) =>
		PagedList<QuestionsCategory>.ToPagedList(await FindAll(trackChanges).ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public void CreateQuestionsCategory(QuestionsCategory questionsCategory) => Create(questionsCategory);
	public void DeleteQuestionsCategory(QuestionsCategory questionsCategory) => Delete(questionsCategory);
}
