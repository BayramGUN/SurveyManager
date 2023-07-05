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
/* using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyManager.Domain.SurveyAggregate.Entities;
public class Answer 
{
    public Guid Id { get; set; }
    [NotMapped]
    public List<string>? Choices { get; set; }

    [Column(TypeName = "varchar(max)")]
    public string ChoicesData
    {
        get => (Choices is not null) ? string.Join(",", Choices) : null!;
        set => Choices = value?.Split(new[] { ',' }).ToList();
    }
    public string? Text { get; private set; }
    public string? QuestionName { get; private set; }
    public int Rate { get; private set; }

} */