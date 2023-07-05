using Microsoft.EntityFrameworkCore;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.SurveyAnswerAggregate;

namespace SurveyManager.Infrastructure.Persistence.Repositories;

public class SurveyAnswerRepository : ISurveyAnswerRepository
{
    private readonly SurveyManagerDbContext _dbContext;
    private static readonly List<SurveyAnswer> _surveyAnswers = new();
    public SurveyAnswerRepository(SurveyManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSurveyAnswerAsync(SurveyAnswer surveyAnswer)
    {
        _surveyAnswers.Add(surveyAnswer);
        await Task.CompletedTask;
        await _dbContext.SurveyAnswers.AddAsync(surveyAnswer);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<List<SurveyAnswer>> GetSurveyAnswersAsync(Guid surveyId)
    {
        /* var surveyAnswer = _surveyAnswers.Where(x => x.SurveyId.Value == surveyId).ToList();
        await Task.CompletedTask;
        return surveyAnswers!; */
        var allSurveyAnswers = await _dbContext.SurveyAnswers.ToListAsync();
        var surveyAnswer = allSurveyAnswers.Where(surveyAnswer => surveyAnswer.SurveyId.Value == surveyId).ToList();
        return surveyAnswer ?? default!; 
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
