using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;

namespace SurveyManager.Domain.SurveyAggregate.Entities;

public class Question : Entity<QuestionId>
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Title { get; private set; }
    public int? RateCount { get; private set; }
    public int? RateMax { get; private set; }
    private readonly List<string>? _choices = new();
    public IReadOnlyList<string>? Choices => _choices!.AsReadOnly();

    private Question(
        QuestionId questionId,
        string name,
        string type,
        string title,
        int rateCount,
        int rateMax,
        List<string>? choices
    ) : base(questionId) 
    {
        Name = name;
        Type = type;
        Title = title;
        RateCount = rateCount;
        RateMax = rateMax;
        _choices = choices;
    }

    public static Question Create(
        string name,
        string type,
        string title,
        int? rateCount = null,
        int? rateMax = null,
        List<string>? choices = null)
    {
        return new (
            QuestionId.CreateUnique(),
            name,
            type,
            title,
            rateCount ?? new(),
            rateMax ?? new(),
            choices ?? new()
        );
    }
# pragma warning disable CS8618
    private Question() { }
# pragma warning disable CS8618
}
