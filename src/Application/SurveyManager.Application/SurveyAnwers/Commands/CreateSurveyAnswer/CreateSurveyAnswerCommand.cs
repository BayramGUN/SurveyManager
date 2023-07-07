using ErrorOr;

using MediatR;

using SurveyManager.Application.Surveys.Commands.CreateSurvey;
using SurveyManager.Domain.SurveyAnswerAggregate;
using SurveyManager.Domain.SurveyAnswerAggregate.Entities;

namespace SurveyManager.Application.SurveyAnswers.Commands.CreateSurveyAnswer;

public record CreateSurveyAnswerCommand(
    Guid SurveyId,
    Guid HostId,
    List<AnswerCommand> Answers) : IRequest<ErrorOr<SurveyAnswer>>;

public record AnswerCommand(
    string QuestionName,
    string Type,
    List<string>? Answer
) : IRequest<ErrorOr<Answer>>;
