using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void AddUser(User user);
}