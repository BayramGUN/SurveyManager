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
    public void Update(
        HostId hostId,
        string title,
        DateTime expiryDate,
        List<Question> questions,
        bool? isActive = null,
        string? description = null)
    {
        HostId = hostId;
        Title = title;
        Description = description;
        IsActive = isActive;
        ExpiryDate = expiryDate;
        UpdatedDateTime = DateTime.UtcNow;
        
        if (questions != null)
        {
            _questions.Clear();
            _questions.AddRange(questions);
        }

    }
    # pragma warning disable CS8618
    private Survey() { }
    # pragma warning disable CS8618
}
/* using SurveyManager.Domain.HostAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;


namespace SurveyManager.Domain.SurveyAggregate;
public class Survey
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; } 
    public List<Question> Questions { get; set; } = new List<Question>();
    public List<Answer>? Answers { get; set; }
    public DateTime ExpiryDate { get; set; }
    public Host? Host { get; set; }
    public Guid HostId { get; set; }
    
} */