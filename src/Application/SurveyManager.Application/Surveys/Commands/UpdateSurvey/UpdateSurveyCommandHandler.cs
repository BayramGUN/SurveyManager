using ErrorOr;

using MediatR;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;

namespace SurveyManager.Application.Surveys.Commands.UpdateSurvey;

public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand, ErrorOr<Survey>>
{
    private readonly ISurveyRepository _surveyRepository;
    public UpdateSurveyCommandHandler(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<ErrorOr<Survey>> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
    {

        var survey = await _surveyRepository.GetSurveyAsync(request.SurveyId);
        
        var surveyUpdate = survey.Update(
            surveyId: SurveyId.Create(survey.Id.Value),
            hostId: HostId.Create(survey.HostId.Value),
            title: survey.Title,
            expiryDate: survey.ExpiryDate,
            survey.CreatedDateTime,
            isActive: request.IsActive,
            questions: survey.Questions.ToList(),
            description: survey.Description
        );
        if(survey is not null && survey.ExpiryDate <= DateTime.UtcNow)
            await _surveyRepository.UpdateSurveyAsync(surveyUpdate);
        
       
        return survey!;
    }
}