using SurveyManager.Domain.Common.Models;

namespace SurveyManager.Domain.SurveyAggregate.Events;

public record SurveyUpdated(Survey Survey) : IDomainEvent;