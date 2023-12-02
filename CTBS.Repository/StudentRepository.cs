using CTBS.Contracts;
using CTBS.Entities;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Repository;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
	public StudentRepository(ApplicationContext applicationContext) 
		: base(applicationContext)
	{
	}

	public async Task<PagedList<Student>> GetAllStudentsAsync(RequestParameters requestParameters, bool trackChanges) =>
		PagedList<Student>.ToPagedList(await FindAll(trackChanges).ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public async Task<Student?> GetStudentAsync(int studentId, bool trackChanges) =>
		await FindByCondition(s => s.Id.Equals(studentId), trackChanges)
			.SingleOrDefaultAsync();

	public void CreateStudent(Student student) => Create(student);
	public void DeleteStudent(Student student) => Delete(student);
}
