using ErrorOr;

using MediatR;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

namespace SurveyManager.Application.Surveys.Commands.CreateSurvey;

public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, ErrorOr<Survey>>
{
    private readonly ISurveyRepository _surveyRepository;
    public CreateSurveyCommandHandler(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<ErrorOr<Survey>> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
    {
        // TODO: Create Survey
        var survey = Survey.Create (
            hostId : HostId.Create(request.HostId),
            title : request.Title,
            expiryDate : request.ExpiryDate,
            questions : request.Questions.ConvertAll(question => Question.Create(
                name : question.Name,
                type : question.Type,
                title : question.Title,
                rateCount : question.RateCount,
                rateMax : question.RateMax,
                choices : question.Choices?.Select(choice => choice.Text).ToList()
            )),
            description : request.Description
            );

        // TODO: Persist Survey
        await _surveyRepository.AddSurveyAsync(survey);
        // TODO: Return Survey

        return survey;
    }
}