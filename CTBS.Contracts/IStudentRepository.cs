using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;

namespace CTBS.Contracts;

public interface IStudentRepository
{
	Task<PagedList<Student>> GetAllStudentsAsync(RequestParameters requestParameters, bool trackChanges);
	Task<Student?> GetStudentAsync(int studentId, bool trackChanges);
	void CreateStudent(Student student);
	void DeleteStudent(Student student);
}