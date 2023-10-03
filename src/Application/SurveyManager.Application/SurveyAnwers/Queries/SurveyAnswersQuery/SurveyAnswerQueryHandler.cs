using ErrorOr;
using MediatR;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.Common.Errors;
using SurveyManager.Application.SurveyAnswers.Common;
using SurveyManager.Application.SurveyAnswers.Queries;

namespace SurveyManager.Application.SurveyAnswers.Queries.SurveyAnswersQuery;

public class SurveyAnswerQueryHandler : 
    IRequestHandler<SurveyAnswersQuery, ErrorOr<SurveyAnswersResult>>
{
    private readonly ISurveyAnswerRepository _surveyAnswerRepository;

    public SurveyAnswerQueryHandler(
        ISurveyAnswerRepository surveyAnswerRepository)
    {
        _surveyAnswerRepository = surveyAnswerRepository;
    }

    public async Task<ErrorOr<SurveyAnswersResult>> Handle(
        SurveyAnswersQuery query,
        CancellationToken cancellationToken)
    {
        var surveyAnswers = await _surveyAnswerRepository.GetSurveyAnswersAsync(query.surveyId);
        //await _surveyAnswerRepository.UpdateSurveyAnswerAsync();
        return(surveyAnswers is null) ? 
            Errors.SurveyAnswers.NotFound:
            new SurveyAnswersResult
            (
                SurveyAnswers: surveyAnswers
            );
    }
}