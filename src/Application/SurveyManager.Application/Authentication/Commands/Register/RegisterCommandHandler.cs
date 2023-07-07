using ErrorOr;
using MediatR;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Application.Common.Services.Authentication;
using SurveyManager.Application.Authentication.Extensions;
using SurveyManager.Domain.UserAggregate;
using SurveyManager.Domain.Common.Errors;
using SurveyManager.Application.Authentication.Common;

namespace SurveyManager.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        if(await _userRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail;
        var hashedPassword = command.Password.GetPasswordHash();
        var user = User.Create(
            command.Firstname,
            command.Lastname,
            command.Email,
            hashedPassword
        );
        await _userRepository.AddUser(user);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            User: user,
            Token: token
        );
        
    }
}