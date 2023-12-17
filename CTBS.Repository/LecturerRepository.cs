using CTBS.Contracts;
using CTBS.Entities;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Repository;

public class LecturerRepository : RepositoryBase<Lecturer>, ILecturerRepository
{
	public LecturerRepository(ApplicationContext applicationContext)
		: base(applicationContext)
	{
	}

	public async Task<PagedList<Lecturer>> GetAllLecturersAsync(RequestParameters requestParameters, bool trackChanges) =>
		PagedList<Lecturer>.ToPagedList(await FindAll(trackChanges)
				.OrderBy(l => l.FirstName)
				.ThenBy(l => l.LastName)
				.ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public async Task<Lecturer?> GetLecturerAsync(int lecturerId, bool trackChanges) =>
		await FindByCondition(l => l.Id.Equals(lecturerId), trackChanges)
			.SingleOrDefaultAsync();

	public void DeleteLecturer(Lecturer lecturer) => Delete(lecturer);
}
