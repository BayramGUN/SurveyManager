using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.SurveyAnswerAggregate.ValueObjects;

namespace SurveyManager.Domain.SurveyAnswerAggregate.Entities;

public class Answer : Entity<AnswerId>
{
    public readonly List<string>? _choices = new();
    public string? QuestionName { get; private set; }
    public string? Type { get; private set; }
    public List<string>? Choices => _choices!;

    private Answer(
        AnswerId answerId,
        string? questionName,
        string? type,
        List<string>? choices
    ) : base(answerId) 
    {
        QuestionName = questionName;
        Type = type;
        _choices = choices;
    }

    public static Answer Create(
        string? questionName,
        string? type,
        List<string>? choices = null)
    {
        return new (
            AnswerId.CreateUnique(),
            questionName ?? string.Empty,
            type ?? string.Empty,
            choices ?? new()
        );
    }
# pragma warning disable CS8618
    private Answer() { }
# pragma warning disable CS8618
}
