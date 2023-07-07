using ErrorOr;
using MediatR;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.Common.Errors;
using SurveyManager.Application.Surveys.Common;

namespace SurveyManager.Application.Surveys.Queries;

public class SurveyAnswerQueryHandler : 
    IRequestHandler<SurveyAnswerQuery, ErrorOr<SurveyAnswerResult>>
{
    private readonly ISurveyAnswerRepository _surveyAnswerRepository;

    public SurveyAnswerQueryHandler(
        ISurveyAnswerRepository surveyAnswerRepository)
    {
        _surveyAnswerRepository = surveyAnswerRepository;
    }

    public async Task<ErrorOr<SurveyAnswerResult>> Handle(
        SurveyAnswerQuery query,
        CancellationToken cancellationToken)
    {
        var surveyAnswers = await _surveyAnswerRepository.GetSurveyAnswersAsync(query.surveyId);
        //await _surveyAnswerRepository.UpdateSurveyAnswerAsync();
        if(surveyAnswers is null)
            return Errors.SurveyAnswers.NotFound;
        return new SurveyAnswerResult
        (
            SurveyAnswers: surveyAnswers
        );
    }
}