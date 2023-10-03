using ErrorOr;
using MediatR;
using SurveyManager.Domain.Common.Errors;
using SurveyManager.Application.Surveys.Common;
using SurveyManager.Application.Common.Interfaces.Persistence;

namespace SurveyManager.Application.Surveys.Queries;

public class HostSurveysQueryHandler : 
    IRequestHandler<HostSurveysQuery, ErrorOr<HostSurveysResult>>
{
    private readonly ISurveyRepository _surveyRepository;

    public HostSurveysQueryHandler(
        ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<ErrorOr<HostSurveysResult>> Handle(
        HostSurveysQuery query,
        CancellationToken cancellationToken)
    {
        var hostSurveys = await _surveyRepository.GetSurveysByHostIdAsync(query.hostId);
        //await _HostSurveysRepository.UpdateHostSurveysAsync();
        return (hostSurveys is null) ? 
            Errors.Survey.NotFound : 
            new HostSurveysResult(HostSurveys: hostSurveys);
    }
}