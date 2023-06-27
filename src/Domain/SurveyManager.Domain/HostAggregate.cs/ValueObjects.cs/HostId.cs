using SurveyManager.Domain.Common.Models;

namespace SurveyManager.Domain.HostAggregate.ValueObjects;

public sealed class HostId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

        private HostId(Guid value)
    {
        Value = value;
    }

    public static HostId CreateUnique()
    {
        return new (Guid.NewGuid());
    }

    public static HostId Create(Guid value)
    {
        return new(value);
    }



    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(HostId data) => data.Value;
}