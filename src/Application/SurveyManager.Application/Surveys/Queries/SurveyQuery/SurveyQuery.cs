using ErrorOr;
using MediatR;
using SurveyManager.Application.Surveys.Common;

namespace SurveyManager.Application.Surveys.Queries;

public record SurveyQuery(
    Guid Id
) : IRequest<ErrorOr<SurveyResult>>;
