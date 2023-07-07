# Domain Models

- [User Domain Model](#user)

## User

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstname": "John",
    "lastname": "Doe",
    "email": "email@example.com",
    "password": "password!"
}
```

```csharp
public sealed class User : AggregateRoot<UserId, Guid>
{
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private User(
        UserId id,
        string firstname,
        string lastname,
        string email,
        string password,
        DateTime createdDateTime,
        DateTime updatedDateTime
    ) : base(id)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static User Create(
        string firstname,
        string lastName,
        string email,
        string password
    )
    {
        return new(
            UserId.CreateUnique(),
            firstname,
            lastName,
            email,
            password,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
# pragma warning disable CS8618
    private User() {}
# pragma warning disable CS8618

}
```

> Note: Password must not be laying around without hashed! ⚠️
