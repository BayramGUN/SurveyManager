using SurveyManager.Domain.SurveyAggregate;

namespace SurveyManager.Application.Common.Interfaces.Persistence;

public interface ISurveyRepository
{
    Task AddSurveyAsync(Survey survey);
    //Task UpdateSurveyAsync();
    Task<Survey> GetSurveyAsync(Guid id);
}