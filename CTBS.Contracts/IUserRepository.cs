using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;

namespace CTBS.Contracts;

public interface IUserRepository
{
	Task<PagedList<User>> GetAllUsersAsync(RequestParameters requestParameters, bool trackChanges);
	Task<User?> GetUserAsync(int userId, bool trackChanges);
	void DeleteUser(User lecturer);
}