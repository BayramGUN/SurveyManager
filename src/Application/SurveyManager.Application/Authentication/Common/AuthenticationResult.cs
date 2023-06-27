using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);