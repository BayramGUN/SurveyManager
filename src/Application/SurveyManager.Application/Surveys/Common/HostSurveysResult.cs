using SurveyManager.Domain.SurveyAggregate;

namespace SurveyManager.Application.Surveys.Common;

public record HostSurveysResult(List<Survey> HostSurveys);