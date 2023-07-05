/* 
using ErrorOr;

using MediatR;

using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

namespace SurveyManager.Application.Surveys.Commands.UpdateSurvey;


public record UpdateSurveyCommand(
    Guid SurveyId,
    Guid HostId,
    string Title,
    string Description,
    bool IsActive,
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

         */
        