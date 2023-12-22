using CTBS.Contracts;
using CTBS.Entities;
using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
	public UserRepository(ApplicationContext applicationContext)
		: base(applicationContext)
	{
	}

	public async Task<PagedList<User>> GetAllUsersAsync(RequestParameters requestParameters, bool trackChanges) =>
		PagedList<User>.ToPagedList(await FindAll(trackChanges)
				.OrderBy(l => l.FirstName)
				.ThenBy(l => l.LastName)
				.ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);

	public async Task<User?> GetUserAsync(int userId, bool trackChanges) =>
		await FindByCondition(l => l.Id.Equals(userId), trackChanges)
			.SingleOrDefaultAsync();

	public void DeleteUser(User user) => Delete(user);
}
