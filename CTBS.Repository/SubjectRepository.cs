using System.Collections.Immutable;
using CTBS.Contracts;
using CTBS.Entities;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Repository;

public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
{
	public SubjectRepository(ApplicationContext applicationContext) 
		: base(applicationContext)
	{
	}

	public async Task<PagedList<Subject>> GetAllSubjectsAsync(RequestParameters requestParameters, bool trackChanges) =>
		PagedList<Subject>.ToPagedList(await FindAll(trackChanges).ToListAsync(), 
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public void CreateSubject(Subject subject) => Create(subject);
	public void DeleteSubject(Subject subject) => Delete(subject);
}
