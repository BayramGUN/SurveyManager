using MapsterMapper;
using MediatR;
using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

using SurveyManager.Application.Surveys.Commands.CreateSurvey;
using SurveyManager.Application.Surveys.Queries;
using SurveyManager.Contracts.Surveys;
using SurveyManager.Application.Surveys.Commands.UpdateSurvey;
using Hangfire;
using SurveyManager.Application.Common.Services;

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
        var createdSurveyId = createSurveyResult.Value.Id.Value;
        var jobId = BackgroundJob.Enqueue<IServiceManagement>((service) => service.SendEmail($"{createdSurveyId}"));
        return createSurveyResult.Match(
            survey => Created("Created", _mapper.Map<SurveyResponse>(survey)),
            errors => Problem(errors)
        );
    } 
    [HttpGet("update")]
    public async Task<IActionResult> UpdateSurvey()
    {
        var id = new Guid("D6D29BA3-B5E4-482D-B135-0AB0B7A270A7");
        var upComm = new UpdateSurveyCommand(
            id,
            false
        );
        var updateSurveyResult = await _mediator.Send(upComm);
        return updateSurveyResult.Match(
            survey => Ok(_mapper.Map<SurveyResponse>(survey)),
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