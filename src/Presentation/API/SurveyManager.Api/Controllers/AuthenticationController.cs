using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using SurveyManager.Contracts.Authentication;
using MediatR;
using SurveyManager.Application.Authentication.Commands.Register;
using MapsterMapper;
using SurveyManager.Api.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Hangfire;
using SurveyManager.Application.Common.Services;

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
        var command = registerRequest.RequestToCommand<RegisterCommand>(_mapper);
        var authResult = await _mediator.Send(command);
        var createdUser = authResult.Value.User;
        var jobId = BackgroundJob.Enqueue<IServiceManagement>((service) => service.SendEmail($"{createdUser.Email}"));
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