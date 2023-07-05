using SurveyManager.Domain.Common.Models;

namespace SurveyManager.Domain.SurveyAnswerAggregate.ValueObjects;

public sealed class AnswerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

        private AnswerId(Guid value)
    {
        Value = value;
    }

    public static AnswerId CreateUnique()
    {
        return new (Guid.NewGuid());
    }

    public static AnswerId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(AnswerId data) => data.Value;
}