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
        /* _surveys.Add(survey);
        await Task.CompletedTask; */
        await _dbContext.Surveys.AddAsync(survey);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Survey> GetSurveyAsync(Guid id)
    {
        /* var survey = _surveys.FirstOrDefault(x => x.Id.Value == id);
        await Task.CompletedTask;
        return survey!; */
        var surveys = await _dbContext.Surveys.ToListAsync();
        var survey = surveys.FirstOrDefault(survey => survey.Id.Value == id);
        return survey ?? default!;
    }

    public async Task<List<Survey>> GetSurveysByHostIdAsync(Guid hostId)
    {
        var surveys = await _dbContext.Surveys.ToListAsync();
        var surveysOfHost = surveys.Where(survey => survey.HostId.Value == hostId).ToList();
        return surveysOfHost;
    }
    /* public async Task UpdateSurveyAsync()
{
   var surveys = await _dbContext.Surveys.ToListAsync();

   var survey = surveys.FirstOrDefault(survey => survey.Id == new Guid("244c95db-865d-4330-87b0-16658db311a3"));
   var udpatedValue = "Description Updated";
   survey!.Update(
       hostId: survey!.HostId,
       title: udpatedValue,
       expiryDate: survey!.ExpiryDate.AddDays(2),
       questions: survey!.Questions.ToList(),
       isActive: false,
       description: udpatedValue
   );

   _dbContext.Update(survey);
   await _dbContext.SaveChangesAsync();
} */
    /* public async Task<IList<Survey>> GetExpiredSurveys(DateTime utcNow)
    {
        var surveysExpired = await _dbContext.Surveys.Where(survey => survey.ExpiryDate > utcNow).ToListAsync();
        return surveysExpired;
    } */
}
