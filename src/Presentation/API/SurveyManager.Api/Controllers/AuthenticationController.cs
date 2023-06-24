using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using SurveyManager.Contracts.Authentication;
using MediatR;
using SurveyManager.Application.Authentication.Commands.Register;
using SurveyManager.Application.Authentication.Queries.Login;
using SurveyManager.Application.Authentication.Common;

namespace SurveyManager.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var command = new RegisterCommand(
            registerRequest.Firstname,
            registerRequest.Lastname,
            registerRequest.Email,
            registerRequest.Password);
        var authResult = await _mediator.Send(command);
        
        return authResult.Match(
            authResult => Created($"{authResult.User.Id}", MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }

    

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
        var authResult = await _mediator.Send(query);
        if(authResult.IsError && authResult.FirstError == Errors.Authentication.CredentialsNotValid)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    Id: authResult.User.Id,
                    Firstname: authResult.User.Firstname,
                    Lastname: authResult.User.Lastname,
                    Email: authResult.User.Email,
                    Token: authResult.Token
                );
    }
}