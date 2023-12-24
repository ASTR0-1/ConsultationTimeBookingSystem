using CTBS.Application.Interfaces;
using CTBS.Application.RequestFeatures;
using CTBS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Infrastructure.Persistence.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
	public UserRepository(ApplicationContext applicationContext)
		: base(applicationContext)
	{
	}

	public async Task<PagedList<User>> GetAllUsersAsync(RequestParameters requestParameters, bool trackChanges)
	{
		return PagedList<User>.ToPagedList(await FindAll(trackChanges)
				.OrderBy(l => l.FirstName)
				.ThenBy(l => l.LastName)
				.ToListAsync(),
			requestParameters.PageNumber,
			requestParameters.PageSize);
	}

	public async Task<User?> GetUserAsync(int userId, bool trackChanges)
	{
		return await FindByCondition(l => l.Id.Equals(userId), trackChanges)
			.SingleOrDefaultAsync();
	}

	public void DeleteUser(User user)
	{
		Delete(user);
	}
}