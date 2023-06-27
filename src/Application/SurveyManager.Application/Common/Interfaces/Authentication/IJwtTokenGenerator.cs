using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Application.Common.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}