﻿using BingoHall.Authorization.JwtTokens.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BingoHall.Authorization.JwtTokens;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IJwtTokenGeneratorConfiguration _configuration;
    public JwtTokenGenerator(IJwtTokenGeneratorConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(RequesteeInfo info)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = Enumerable.Concat(
            new[]
            {
                new Claim(ClaimTypes.Email, info.Email)
            },
            info.Roles?.Select(role => new Claim(ClaimTypes.Role, role)) ?? Enumerable.Empty<Claim>()
            )
            .ToArray();
        
        var token = new JwtSecurityToken(_configuration.Issuer,
            _configuration.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
