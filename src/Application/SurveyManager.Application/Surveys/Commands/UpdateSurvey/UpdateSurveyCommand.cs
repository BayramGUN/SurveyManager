
using ErrorOr;

using MediatR;

using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

namespace SurveyManager.Application.Surveys.Commands.UpdateSurvey;


public record UpdateSurveyCommand(
    Guid SurveyId,
    bool IsActive
) : IRequest<ErrorOr<Survey>>;
        