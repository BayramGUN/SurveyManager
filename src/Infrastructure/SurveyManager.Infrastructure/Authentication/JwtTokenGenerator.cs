using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyManager.Application.Common.Interfaces.Services;
using SurveyManager.Application.Common.Services.Authentication;
using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{ 
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _tokenSettings;

    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _tokenSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenSettings.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );
 
        var claims = new[] 
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString() ?? null!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Firstname),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.Lastname),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var securityToken = new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_tokenSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}