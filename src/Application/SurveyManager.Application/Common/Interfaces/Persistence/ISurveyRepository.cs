using SurveyManager.Domain.SurveyAggregate;

namespace SurveyManager.Application.Common.Interfaces.Persistence;

public interface ISurveyRepository
{
    Task AddSurveyAsync(Survey survey);
    Task UpdateSurveysAsync(List<Survey> surveys);
    Task UpdateSurveyAsync(Survey survey);
    Task<Survey> GetSurveyAsync(Guid id);
    Task<List<Survey>> GetSurveysByHostIdAsync(Guid hostId);
    Task<List<Survey>> GetAllSurveysAsync();
    
}