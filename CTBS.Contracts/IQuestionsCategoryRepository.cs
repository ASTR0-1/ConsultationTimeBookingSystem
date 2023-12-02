﻿using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;

namespace CTBS.Contracts;

public interface IQuestionsCategoryRepository
{
	Task<PagedList<QuestionsCategory>> GetAllQuestionsCategoriesAsync(RequestParameters requestParameters, bool trackChanges);
	void CreateQuestionsCategory(QuestionsCategory questionsCategory);
	void DeleteQuestionsCategory(QuestionsCategory questionsCategory);
}