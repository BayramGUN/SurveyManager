using ErrorOr;
using MediatR;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.Common.Errors;
using SurveyManager.Application.Surveys.Common;

namespace SurveyManager.Application.Surveys.Queries;

public class SurveyQueryHandler : 
    IRequestHandler<SurveyQuery, ErrorOr<SurveyResult>>
{
    private readonly ISurveyRepository _surveyRepository;

    public SurveyQueryHandler(
        ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<ErrorOr<SurveyResult>> Handle(
        SurveyQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var survey = _surveyRepository.GetSurvey(new Guid(query.Id));
        if(survey is null)
            return Errors.Survey.NotFound;
        return new SurveyResult
        (
            Survey: survey
        );
    }
}