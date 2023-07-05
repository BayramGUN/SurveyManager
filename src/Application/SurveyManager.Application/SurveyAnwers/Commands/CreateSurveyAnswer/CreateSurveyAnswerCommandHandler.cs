

using ErrorOr;

using MediatR;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAnswerAggregate;
using SurveyManager.Domain.SurveyAnswerAggregate.Entities;

namespace SurveyManager.Application.SurveyAnswers.Commands.CreateSurveyAnswer;

public class CreateSurveyAnswerCommandHandler : IRequestHandler<CreateSurveyAnswerCommand, ErrorOr<SurveyAnswer>>
{
    private readonly ISurveyAnswerRepository _surveyAnswerRepository;
    public CreateSurveyAnswerCommandHandler(ISurveyAnswerRepository surveyAnswerRepository)
    {
        _surveyAnswerRepository = surveyAnswerRepository;
    }

    public async Task<ErrorOr<SurveyAnswer>> Handle(CreateSurveyAnswerCommand request, CancellationToken cancellationToken)
    {
        // TODO: Create Survey
        var surveyAnswer = SurveyAnswer.Create(
            surveyId: SurveyId.Create(request.SurveyId),
            hostId: HostId.Create(request.HostId),
            answers: request.Answers.ConvertAll(answer => Answer.Create(
                questionName: answer.QuestionName,
                type: answer.Type,
                choices: answer.Answer?.ToList()
            ))
        );
        // TODO: Persist Survey
        await _surveyAnswerRepository.AddSurveyAnswerAsync(surveyAnswer);
        // TODO: Return Survey

        return surveyAnswer;
    }
}