using ErrorOr;
using MediatR;
using SurveyManager.Application.Authentication.Common;

namespace SurveyManager.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Firstname,
    string Lastname,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;