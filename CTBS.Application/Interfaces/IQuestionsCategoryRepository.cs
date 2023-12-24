using CTBS.Application.RequestFeatures;
using CTBS.Domain.Models;

namespace CTBS.Application.Interfaces;

public interface IQuestionsCategoryRepository
{
	Task<PagedList<QuestionsCategory>> GetAllQuestionsCategoriesAsync(RequestParameters requestParameters,
		bool trackChanges);

	Task<QuestionsCategory?> GetQuestionsCategoryAsync(int questionsCategoryId, bool trackChanges);
	void CreateQuestionsCategory(QuestionsCategory questionsCategory);
	void DeleteQuestionsCategory(QuestionsCategory questionsCategory);
}