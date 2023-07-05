using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.SurveyAnswerAggregate;

namespace SurveyManager.Domain.SurveyAnswerAggregate.Events;

public record SurveyAnswerCreated(SurveyAnswer SurveyAnswer) : IDomainEvent;