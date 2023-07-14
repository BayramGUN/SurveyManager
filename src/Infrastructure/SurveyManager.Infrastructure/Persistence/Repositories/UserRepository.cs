using Microsoft.EntityFrameworkCore;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SurveyManagerDbContext _context;

    public UserRepository(SurveyManagerDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
        return user;
    }
}