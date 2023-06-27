using SurveyManager.Domain.Common.Models;

namespace SurveyManager.Domain.SurveyAggregate.ValueObjects;

public sealed class SurveyId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

        private SurveyId(Guid value)
    {
        Value = value;
    }

    public static SurveyId CreateUnique()
    {
        return new (Guid.NewGuid());
    }

    public static SurveyId Create(Guid value)
    {
        return new(value);
    }



    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(SurveyId data) => data.Value;
}