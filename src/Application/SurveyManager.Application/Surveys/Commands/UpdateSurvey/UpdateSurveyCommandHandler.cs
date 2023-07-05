/* using ErrorOr;

using MediatR;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

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

        // TODO: Create Survey
        var survey = _surveyRepository.GetSurveyAsync(request.SurveyId);
        survey.Update(
            hostId: HostId.Create(request.HostId),
            title: request.Title,
            expiryDate: request.ExpiryDate,
            isActive: request.IsActive,
            description: request.Description,
            questions: request.Questions.ConvertAll(question => Question.Create(
                name: question.Name,
                type: question.Type,
                rateCount: question.RateCount,
                rateMax: question.RateMax,
                choices: question.Choices?.ToList()
            ))
        );
        // TODO: Persist Survey
        await _surveyRepository.UpdateSurveyAsync(survey);
        // TODO: Return Survey

        return survey;
    }
} */