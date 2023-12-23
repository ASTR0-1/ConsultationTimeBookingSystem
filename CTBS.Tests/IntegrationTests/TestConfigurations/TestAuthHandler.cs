using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CTBS.Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CTBS.Tests.IntegrationTests.TestConfigurations;

public class TestAuthHandler
{
	public static string CreateToken(IConfiguration configuration)
	{
		var signingCredentials = GetSigningCredentials(configuration);
		var claims = GetClaims();
		var tokenOptions = GenerateTokeOptions(signingCredentials, claims, configuration);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}

	private static List<Claim> GetClaims()
	{
		var claims = new List<Claim>
		{
			new (ClaimTypes.Name, "User")
		};

		claims.AddRange(new List<Claim>
		{
			new(ClaimTypes.Role, "Lecturer"),
			new(ClaimTypes.Role, "Student"),
		});

		return claims;
	}

	private static JwtSecurityToken GenerateTokeOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims,
		IConfiguration configuration) =>
		new(
			issuer: configuration.GetSection("JwtSettings").GetSection("validIssuer").Value,
			audience: configuration.GetSection("JwtSettings").GetSection("validAudience").Value,
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration.GetSection("JwtSettings").GetSection("expires").Value)),
			signingCredentials: signingCredentials
		);

	private static SigningCredentials GetSigningCredentials(IConfiguration configuration)
	{
		var key = Encoding.UTF8.GetBytes(configuration.GetSection("JwtSecret").Value);
		var secret = new SymmetricSecurityKey(key);

		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}
}
