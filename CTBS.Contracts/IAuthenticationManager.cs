using CTBS.Entities.DataTransferObjects.Authentication;

namespace CTBS.Contracts;

public interface IAuthenticationManager
{
	Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication);
	Task<string> CreateTokenAsync();
}
