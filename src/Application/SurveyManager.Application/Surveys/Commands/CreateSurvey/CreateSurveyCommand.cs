using ErrorOr;

using MediatR;

using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

namespace SurveyManager.Application.Surveys.Commands.CreateSurvey;


public record CreateSurveyCommand(
    Guid HostId,
    string Title,
    string Description,
    bool IsActive,
    DateTime ExpiryDate,
    List<QuestionCommand> Questions) : IRequest<ErrorOr<Survey>>;

public record QuestionCommand(
    string Name,
    string Type,
    string Title,
    int? RateCount,
    int? RateMax,
    List<Choice>? Choices
); // : IRequest<ErrorOr<Question>>;

public record Choice(
    string Value,
    string Text
);