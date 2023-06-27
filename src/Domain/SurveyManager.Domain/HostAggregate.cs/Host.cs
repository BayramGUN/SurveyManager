using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;
using SurveyManager.Domain.UserAggregate.ValueObjects;

namespace SurveyManager.Domain.HostAggregate;

public sealed class Host : AggregateRoot<HostId, Guid>
{
    private readonly List<SurveyId> _surveyIds = new();

    public string Firstname { get; }
    public string Lastname { get; }
    public string Email { get; }
    public string ProfileImage { get; }
    public UserId UserId { get; }

    public IReadOnlyList<SurveyId> SurveyIds  => _surveyIds.AsReadOnly();

    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private Host(
        HostId hostId,
        string firstname,
        string lastname,
        string profileImage,
        string email,
        UserId userId,
        DateTime createdDateTime,
        DateTime updatedDateTime
    ) : base(hostId)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        ProfileImage = profileImage;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Host Create(
        string firstname,
        string lastName,
        string email,
        string profileImage,
        UserId userId)
    {
        return new (
            HostId.CreateUnique(),
            firstname,
            lastName,
            email,
            profileImage,
            userId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}