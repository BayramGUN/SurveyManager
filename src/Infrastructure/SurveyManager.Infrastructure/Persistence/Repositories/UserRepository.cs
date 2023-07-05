using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(user => user.Email == email);
    }
}