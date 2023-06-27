using ErrorOr;

using MediatR;

using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

namespace SurveyManager.Application.Surveys.Commands.CreateSurvey;


public record CreateSurveyCommand(
    Guid HostId,
    string Title,
    string Description,
    DateTime ExpiryDate,
    List<QuestionCommand> Questions) : IRequest<ErrorOr<Survey>>;

public record QuestionCommand(
    string Name,
    string Title,
    string Type,
    int? RateCount,
    int? RateMax,
    List<string>? Choices
); // : IRequest<ErrorOr<Question>>;
