namespace SurveyManager.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
where TId : notnull
{
    private readonly List<IDomainEvent> _doaminEvents = new();
    public TId Id { get; protected set; }
    public IReadOnlyList<IDomainEvent> DomainEvents => _doaminEvents.AsReadOnly();
    public Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }
    
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator == (Entity<TId> left, Entity<TId> rigth)
    {
        return Equals(left, rigth);
    }
    public static bool operator != (Entity<TId> left, Entity<TId> rigth)
    {
        return !Equals(left, rigth);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _doaminEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _doaminEvents.Clear();
    }
# pragma warning disable CS8618
    protected Entity() {}
# pragma warning disable CS8618
}
