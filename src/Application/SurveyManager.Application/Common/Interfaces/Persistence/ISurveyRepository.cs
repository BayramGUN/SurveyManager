using SurveyManager.Domain.SurveyAggregate;

namespace SurveyManager.Application.Common.Interfaces.Persistence;

public interface ISurveyRepository
{
    void AddSurvey(Survey user);
    Survey GetSurvey(Guid id);
}