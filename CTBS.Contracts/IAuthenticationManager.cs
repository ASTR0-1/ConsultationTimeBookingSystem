using CTBS.Entities.DataTransferObjects;

namespace CTBS.Contracts;

public interface IAuthenticationManager
{
	Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication);
	Task<string> CreateToken();
}
