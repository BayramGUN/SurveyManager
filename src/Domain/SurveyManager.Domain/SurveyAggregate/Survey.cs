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
    public bool? IsActive { get; private set; }
    
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
        bool isActive,
        DateTime expiryDate,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<Question> questions
    ) : base(surveyId)
    {
        HostId = hostId;
        Title = title;
        Description = description;
        IsActive = isActive;
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
        bool? isActive = null,
        string? description = null)
    {
        var survey = new Survey(
            SurveyId.CreateUnique(),
            hostId,
            title,
            description ?? string.Empty,
            isActive ?? true,
            expiryDate,
            DateTime.UtcNow,
            DateTime.UtcNow,
            questions ?? new());

        survey.AddDomainEvent(new SurveyCreated(survey));

        return survey;
    }
    public Survey Update(
        SurveyId surveyId,
        HostId hostId,
        string title,
        DateTime expiryDate,
        DateTime createdDateTime,
        List<Question> questions,
        bool isActive,
        string? description = null)
    {

        if (questions is not null)
        {
            _questions.Clear();
            _questions.AddRange(questions);
        }
        var survey = new Survey(
            surveyId,
            hostId,
            title,
            description ?? string.Empty,
            isActive,
            expiryDate,
            createdDateTime,
            DateTime.UtcNow,
            questions ?? new());

        survey.AddDomainEvent(new SurveyUpdated(survey));
        return survey;
    }
    # pragma warning disable CS8618
    private Survey() { }
    # pragma warning disable CS8618
}
