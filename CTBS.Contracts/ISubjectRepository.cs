using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;

namespace CTBS.Contracts;

public interface ISubjectRepository
{
	Task<PagedList<Subject>> GetAllSubjectsAsync(RequestParameters requestParameters, bool trackChanges);
	Task<Subject?> GetSubjectAsync(int subjectId, bool trackChanges);
	void CreateSubject(Subject subject);
	void DeleteSubject(Subject subject);
}