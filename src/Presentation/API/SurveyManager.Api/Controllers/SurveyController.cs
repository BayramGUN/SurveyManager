using MapsterMapper;
using MediatR;
using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SurveyManager.Application.Surveys.Commands.CreateSurvey;
using SurveyManager.Application.Surveys.Queries;
using SurveyManager.Contracts.Surveys;

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

    [HttpGet("survey")]
    public async Task<IActionResult> GetSurveyByHost([FromQuery]Guid id)
    {
        var query = new SurveyQuery(Id: id);
        var surveyResult = await _mediator.Send(query);
        if(surveyResult.IsError && surveyResult.FirstError == Errors.Survey.NotFound)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: surveyResult.FirstError.Description
            );
        }
        return surveyResult.Match(
            surveyResult => Ok(_mapper.Map<SurveyResponse>(surveyResult)),
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
}