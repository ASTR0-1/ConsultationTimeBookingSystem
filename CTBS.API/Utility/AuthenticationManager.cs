﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CTBS.Contracts;
using CTBS.Entities.DataTransferObjects.Authentication;
using CTBS.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CTBS.API.Utility;

public class AuthenticationManager : IAuthenticationManager
{
	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;
	
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

	private JwtSecurityToken GenerateTokeOptions(SigningCredentials signingCredentials, List<Claim> claims) =>
		new(
			issuer: _configuration.GetSection("JwtSettings").GetSection("validIssuer").Value,
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.GetSection("JwtSettings").GetSection("expires")))
		);

	private async Task<List<Claim>> GetClaims()
	{
		var claims = new List<Claim>
		{
			new (ClaimTypes.Name, _user.UserName)
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