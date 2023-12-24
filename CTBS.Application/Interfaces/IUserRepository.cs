using CTBS.Application.RequestFeatures;
using CTBS.Domain.Models;

namespace CTBS.Application.Interfaces;

public interface IUserRepository
{
	Task<PagedList<User>> GetAllUsersAsync(RequestParameters requestParameters, bool trackChanges);
	Task<User?> GetUserAsync(int userId, bool trackChanges);
	void DeleteUser(User lecturer);
}