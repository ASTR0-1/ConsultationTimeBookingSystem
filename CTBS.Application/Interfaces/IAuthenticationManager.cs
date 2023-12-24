using CTBS.Application.DataTransferObjects.Authentication;

namespace CTBS.Application.Interfaces;

public interface IAuthenticationManager
{
	Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication);
	Task<string> CreateTokenAsync();
}