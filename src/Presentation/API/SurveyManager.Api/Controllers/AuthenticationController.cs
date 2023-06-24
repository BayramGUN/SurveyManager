using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using SurveyManager.Contracts.Authentication;
using MediatR;
using SurveyManager.Application.Authentication.Commands.Register;
using SurveyManager.Application.Authentication.Queries.Login;
using SurveyManager.Application.Authentication.Common;
using MapsterMapper;
using SurveyManager.Api.Common.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace SurveyManager.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var command = registerRequest.RegisterRequestToCommand(_mapper);
        var authResult = await _mediator.Send(command);
        
        return authResult.Match(
            authResult => Created(
                $"{authResult.User.Id}",
                authResult.AuthenticationResultToResponse(_mapper)),
            errors => Problem(errors)
        );
    }

    

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = loginRequest.LoginRequestToQuery(_mapper);
        var authResult = await _mediator.Send(query);
        if(authResult.IsError && authResult.FirstError == Errors.Authentication.CredentialsNotValid)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }
        return authResult.Match(
            authResult => Ok(authResult.AuthenticationResultToResponse(_mapper)),
            errors => Problem(errors)
        );
    } 
}