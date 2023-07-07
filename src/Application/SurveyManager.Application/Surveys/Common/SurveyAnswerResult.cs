using SurveyManager.Domain.SurveyAnswerAggregate;

namespace SurveyManager.Application.Surveys.Common;

public record SurveyAnswerResult(List<SurveyAnswer> SurveyAnswers);