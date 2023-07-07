using MapsterMapper;
using MediatR;
using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SurveyManager.Application.Surveys.Commands.CreateSurvey;
using SurveyManager.Application.Surveys.Queries;
using SurveyManager.Contracts.Surveys;
using SurveyManager.Application.Common.Interfaces.Persistence;

namespace SurveyManager.Api.Controllers;


[Route("hosts/{hostId}/[controller]s")]
public class SurveyController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public SurveyController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSurveyByHost(Guid hostId)
    {
        var query = new HostSurveysQuery(hostId: hostId);
        var hostSurveysResult = await _mediator.Send(query);
        if(hostSurveysResult.IsError && hostSurveysResult.FirstError == Errors.Survey.NotFound)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: hostSurveysResult.FirstError.Description
            );
        }
        //var responses = _mapper.Map<List<SurveyResponse>>(hostSurveysResult);
        return hostSurveysResult.Match(
            hostSurveysResult => Ok(hostSurveysResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateSurvey(
        CreateSurveyRequest surveyRequest,
        Guid hostId
    )
    {
        var command = _mapper.Map<CreateSurveyCommand>((surveyRequest, hostId));

        var createSurveyResult = await _mediator.Send(command);
        
        return createSurveyResult.Match(
            survey => Created($"Survey created the link is = http://127.0.0.1:5500/surveyPage.html?={survey.Id}", _mapper.Map<SurveyResponse>(survey)),
            errors => Problem(errors)
        );
    } 

    [HttpGet("getAnswers")]
    public async Task<IActionResult> GetAllAnswers([FromQuery]Guid surveyId)
    {
        var query = new SurveyAnswerQuery(surveyId: surveyId);

        var getSurveyAnswerResult = await _mediator.Send(query);
        
        return getSurveyAnswerResult.Match(
            hostSurveysResult => Ok(hostSurveysResult),
            errors => Problem(errors)
        );
    }

}