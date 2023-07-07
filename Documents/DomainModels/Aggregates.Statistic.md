# Doamin Models

- [Answer Domain Model](#answer)

## Answer

- Class looks in C#

```csharp
using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAnswerAggregate.Entities;
using SurveyManager.Domain.SurveyAnswerAggregate.Events;
using SurveyManager.Domain.SurveyAnswerAggregate.ValueObjects;

namespace SurveyManager.Domain.SurveyAnswerAggregate;

public class SurveyAnswer : AggregateRoot<SurveyAnswerId, Guid>
{
    private readonly List<Answer> _answers = new();
    
    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();
    public HostId HostId { get; private set; }
    public SurveyId SurveyId { get; private set; }

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private SurveyAnswer(
        SurveyAnswerId surveyAnswerId,
        HostId hostId,
        SurveyId surveyId,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<Answer> answers
    ) : base(surveyAnswerId)
    {
        SurveyId = surveyId;
        HostId = hostId;
        _answers = answers;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static SurveyAnswer Create(
            List<Answer> answers,
            SurveyId surveyId,
            HostId hostId
        )
    {
        var surveyAnswer = new SurveyAnswer(
            SurveyAnswerId.CreateUnique(),
            hostId,
            surveyId,
            DateTime.UtcNow,
            DateTime.UtcNow,
            answers ?? new());

        surveyAnswer.AddDomainEvent(new SurveyAnswerCreated(surveyAnswer));

        return surveyAnswer;
    }
    public void Update(
        DateTime expiryDate,
        List<Answer> answers)
    {
        UpdatedDateTime = DateTime.UtcNow;
        
        if (answers != null)
        {
            _answers.Clear();
            _answers.AddRange(answers);
        }

    }
    # pragma warning disable CS8618
    private SurveyAnswer() { }
    # pragma warning disable CS8618
}
```

- Class looks in JSON:

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "title",
    "description": "description",
    "hostId": "00000000-0000-0000-0000-000000000000",
    "questions": [
        {
            "question": "a question example",
            "answers": [
                "answer-1",
                "answer-2"
            ]
        },
        {
            "question": "a question example two",
            "answers": [
                "answer-1",
                "answer-2"
            ]
        },
        {
            "question": "a question example three",
            "mostPopularAnswer": "answer-1"
        },
        {
            "question": "a question example four",
            "rateOfAnswers": 3.8
        }
    ],
}
```
