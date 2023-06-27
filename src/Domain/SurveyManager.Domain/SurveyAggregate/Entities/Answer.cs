/* 
using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;

namespace SurveyManager.Domain.SurveyAggregate.Entities;

public class Question : Entity<QuestionId>
{
    public string Name { get; private set; }
    public string Title { get; private set; }
    public string Type { get; private set; }
    public int? RateCount { get; private set; }
    public int? RateMax { get; private set; }
    public readonly List<string> _choises = new();

    private Question(
        QuestionId questionId,
        string name,
        string title,
        string type,
        int rateCount,
        int rateMax,
        List<string> choises
    ) : base(questionId) 
    {
        Name = name;
        Title = title;
        Type = type;
        RateCount = rateCount;
        RateMax = rateMax;
        _choises = choises;
    }

    public static Question Create(
        string name,
        string title,
        string type,
        int? rateCount,
        int? rateMax,
        List<string>? choises = null)
    {
        return new (
            QuestionId.CreateUnique(),
            name,
            title,
            type,
            rateCount ?? new(),
            rateMax ?? new(),
            choises ?? new()
        );
    }
# pragma warning disable CS8618
    private Question() { }
# pragma warning disable CS8618
} */