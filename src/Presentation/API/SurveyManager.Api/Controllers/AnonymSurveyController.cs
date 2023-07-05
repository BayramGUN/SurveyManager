using MapsterMapper;
using MediatR;
using SurveyManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SurveyManager.Application.Surveys.Queries;
using SurveyManager.Contracts.Surveys;
using SurveyManager.Contracts.SurveyAnswers;
using SurveyManager.Application.SurveyAnswers.Commands.CreateSurveyAnswer;
using SurveyManager.Api.Common.Extensions;
using SurveyManager.Application.Common.Interfaces.Persistence;

namespace SurveyManager.Api.Controllers;


[Route("[controller]s")]
[AllowAnonymous]
public class AnonymSurveyController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly ISurveyAnswerRepository _repository;

    public AnonymSurveyController(IMapper mapper, ISender mediator, ISurveyAnswerRepository repository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _repository = repository;
    }

    [HttpGet("survey")]
    public async Task<IActionResult> GetAnonymSurvey([FromQuery]Guid id)
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

    [HttpPost("answers")]
    public async Task<IActionResult> CreateAnswers(CreateAnswerRequest surveyAnswerRequest)
    {
        var command = surveyAnswerRequest.RequestToCommand<CreateSurveyAnswerCommand>(_mapper);

        var createSurveyAnswerResult = await _mediator.Send(command);
        
        return createSurveyAnswerResult.Match(
            surveyAnswer => Created(
                surveyAnswer.Id.Value.ToString(), _mapper.Map<SurveyAnswerResponse>(surveyAnswer)
            ),
            errors => Problem(errors)
        );
    }

    [HttpGet("getanswers")]
    public async Task<IActionResult> GetAllAnswers([FromQuery]Guid surveyId)
    {
        var answers = await _repository.GetSurveyAnswersAsync(surveyId);
        return Ok(answers);
    }

    /* [HttpPut("answer")]
    public async Task<IActionResult> AnswerSurvey(
        CreateSurveyRequest surveyRequest,
        Guid hostId
    )
    {
        var command = _mapper.Map<CreateSurveyCommand>((surveyRequest, hostId));

        var createSurveyResult = await _mediator.Send(command);
        
        return createSurveyResult.Match(
            survey => Created($"Survey created the link is = http://127.0.0.1:5500/surveyPage.html?={survey.Id.Value}", _mapper.Map<SurveyResponse>(survey)),
            errors => Problem(errors)
        );
    }  */
}