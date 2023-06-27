using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate.Entities;
using SurveyManager.Domain.SurveyAggregate.Events;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;

namespace SurveyManager.Domain.SurveyAggregate;

public class Survey : AggregateRoot<SurveyId, Guid>
{
    private readonly List<Question> _questions = new();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    
    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();
    public HostId HostId { get; private set; }

    public DateTime ExpiryDate { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Survey(
        SurveyId surveyId,
        HostId hostId,
        string title,
        string description,
        DateTime expiryDate,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<Question> questions
    ) : base(surveyId)
    {
        HostId = hostId;
        Title = title;
        Description = description;
        _questions = questions;
        ExpiryDate = expiryDate;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Survey Create(
        HostId hostId,
        string title,
        DateTime expiryDate,
        List<Question> questions,
        string? description = null)
    {
        var survey = new Survey(
            SurveyId.CreateUnique(),
            hostId,
            title,
            description ?? string.Empty,
            expiryDate,
            DateTime.UtcNow,
            DateTime.UtcNow,
            questions ?? new());
        survey.AddDomainEvent(new SurveyCreated(survey));

        return survey;
    }
}