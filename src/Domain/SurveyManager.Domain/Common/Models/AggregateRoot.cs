namespace SurveyManager.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set;} = null!;

    protected AggregateRoot(TId id)
    {
        Id = id;
    }

    protected AggregateRoot()
    {

    }
}