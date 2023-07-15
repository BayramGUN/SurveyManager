using Microsoft.EntityFrameworkCore;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;

namespace SurveyManager.Infrastructure.Persistence.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly SurveyManagerDbContext _dbContext;
    //private static readonly List<Survey> _surveys = new();
    public SurveyRepository(SurveyManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSurveyAsync(Survey survey)
    {
        await _dbContext.Surveys.AddAsync(survey);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Survey> GetSurveyAsync(Guid id)
    {        
        var surveys = await _dbContext.Surveys.AsNoTrackingWithIdentityResolution().ToListAsync();
        var survey = surveys.FirstOrDefault(survey => survey.Id.Value == id);
        return survey ?? default!;
    }

    public async Task<List<Survey>> GetSurveysByHostIdAsync(Guid hostId)
    {
        var surveys = await _dbContext.Surveys.AsNoTrackingWithIdentityResolution().ToListAsync();
        var hostSurveys = surveys.Where(survey => survey.HostId.Value == hostId).ToList();
        return hostSurveys;
    }
    public async Task<List<Survey>> GetAllSurveysAsync()
    {
        var surveys = await _dbContext.Surveys.AsNoTrackingWithIdentityResolution().ToListAsync();
        return surveys;
    }
    public async Task UpdateSurveyAsync(Survey survey)
    {
       _dbContext.Update(survey);
       await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateSurveysAsync(List<Survey> surveys)
    {
        _dbContext.Surveys.UpdateRange(surveys);
        await _dbContext.SaveChangesAsync();
    }
    /* public async Task<IList<Survey>> GetExpiredSurveys(DateTime utcNow)
{
   var surveysExpired = await _dbContext.Surveys.Where(survey => survey.ExpiryDate > utcNow).ToListAsync();
   return surveysExpired;
} */
}
