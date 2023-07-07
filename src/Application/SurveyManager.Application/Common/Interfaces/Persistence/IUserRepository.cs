using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task AddUser(User user);
}