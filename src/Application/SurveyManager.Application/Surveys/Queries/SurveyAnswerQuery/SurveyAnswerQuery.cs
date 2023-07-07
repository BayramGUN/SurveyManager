using ErrorOr;
using MediatR;
using SurveyManager.Application.Surveys.Common;

namespace SurveyManager.Application.Surveys.Queries;

public record SurveyAnswerQuery(
    Guid surveyId
) : IRequest<ErrorOr<SurveyAnswerResult>>;
