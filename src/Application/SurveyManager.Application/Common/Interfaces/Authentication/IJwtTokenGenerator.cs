using SurveyManager.Domain.Entities;

namespace SurveyManager.Application.Common.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}