using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CTBS.Application.DataTransferObjects.Authentication;
using CTBS.Application.Interfaces;
using CTBS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CTBS.Infrastructure.Identity;

public class AuthenticationManager : IAuthenticationManager
{
	private readonly IConfiguration _configuration;
	private readonly UserManager<User> _userManager;

	private User? _user;

	public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
	{
		_userManager = userManager;
		_configuration = configuration;
	}

	public async Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication)
	{
		_user = await _userManager.FindByEmailAsync(userForAuthentication.Email);
		var passwordCheckResult = await _userManager.CheckPasswordAsync(_user, userForAuthentication.Password);

		return _user is not null
		       && passwordCheckResult;
	}

	public async Task<string> CreateTokenAsync()
	{
		var signingCredentials = GetSigningCredentials();
		var claims = await GetClaims();
		var tokenOptions = GenerateTokeOptions(signingCredentials, claims);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}

	private JwtSecurityToken GenerateTokeOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
	{
		return new JwtSecurityToken(
			_configuration.GetSection("JwtSettings").GetSection("validIssuer").Value,
			_configuration.GetSection("JwtSettings").GetSection("validAudience").Value,
			claims,
			expires: DateTime.Now.AddMinutes(
				Convert.ToDouble(_configuration.GetSection("JwtSettings").GetSection("expires").Value)),
			signingCredentials: signingCredentials
		);
	}

	private async Task<List<Claim>> GetClaims()
	{
		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, _user.UserName)
		};

		var roles = await _userManager.GetRolesAsync(_user);

		claims.AddRange(roles.Select(role =>
			new Claim(ClaimTypes.Role, role)));

		return claims;
	}

	private SigningCredentials GetSigningCredentials()
	{
		var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSecret").Value);
		var secret = new SymmetricSecurityKey(key);

		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}
}