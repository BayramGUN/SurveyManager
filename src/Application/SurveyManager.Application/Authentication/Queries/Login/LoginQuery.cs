using ErrorOr;
using MediatR;
using SurveyManager.Application.Authentication.Common;

namespace SurveyManager.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
