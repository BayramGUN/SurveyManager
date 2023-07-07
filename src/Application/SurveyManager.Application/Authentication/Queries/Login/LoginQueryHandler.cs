using ErrorOr;
using MediatR;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Application.Common.Services.Authentication;
using SurveyManager.Application.Authentication.Extensions;
using SurveyManager.Domain.Common.Errors;
using SurveyManager.Application.Authentication.Common;

namespace SurveyManager.Application.Authentication.Queries.Login;

public class LoginQueryHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        LoginQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var user = await _userRepository.GetUserByEmail(query.Email);
        if(user is null || !query.Password.VerifyPassword(user.Password))
            return Errors.Authentication.CredentialsNotValid;
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }
}