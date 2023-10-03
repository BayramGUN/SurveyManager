using ErrorOr;
using MediatR;

using SurveyManager.Application.SurveyAnswers.Common;

namespace SurveyManager.Application.SurveyAnswers.Queries.SurveyAnswersQuery;

public record SurveyAnswersQuery(
    Guid surveyId
) : IRequest<ErrorOr<SurveyAnswersResult>>;
