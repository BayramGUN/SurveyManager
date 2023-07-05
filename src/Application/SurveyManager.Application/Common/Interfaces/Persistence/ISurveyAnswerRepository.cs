using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAnswerAggregate;

namespace SurveyManager.Application.Common.Interfaces.Persistence;

public interface ISurveyAnswerRepository
{
    Task AddSurveyAnswerAsync(SurveyAnswer surveyAnswer);
    //Task UpdateSurveyAnswerAsync();
    Task<List<SurveyAnswer>> GetSurveyAnswersAsync(Guid surveyId);
}