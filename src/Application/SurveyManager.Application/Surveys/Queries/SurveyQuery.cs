using ErrorOr;
using MediatR;
using SurveyManager.Application.Surveys.Common;

namespace SurveyManager.Application.Surveys.Queries;

public record SurveyQuery(
    string Id
) : IRequest<ErrorOr<SurveyResult>>;
