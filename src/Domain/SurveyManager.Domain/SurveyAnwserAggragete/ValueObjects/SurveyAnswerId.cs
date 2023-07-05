using SurveyManager.Domain.Common.Models;

namespace SurveyManager.Domain.SurveyAnswerAggregate.ValueObjects;

public sealed class SurveyAnswerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SurveyAnswerId(Guid value)
    {
        Value = value;
    }

    public static SurveyAnswerId CreateUnique()
    {
        return new (Guid.NewGuid());
    }

    public static SurveyAnswerId Create(Guid value)
    {
        return new(value);
    }



    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(SurveyAnswerId data) => data.Value;
}