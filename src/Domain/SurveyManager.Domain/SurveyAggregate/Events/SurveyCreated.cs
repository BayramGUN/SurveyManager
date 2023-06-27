using SurveyManager.Domain.Common.Models;

namespace SurveyManager.Domain.SurveyAggregate.Events;

public record SurveyCreated(Survey Survey) : IDomainEvent;