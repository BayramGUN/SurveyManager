using SurveyManager.Domain.Entities;

namespace SurveyManager.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);