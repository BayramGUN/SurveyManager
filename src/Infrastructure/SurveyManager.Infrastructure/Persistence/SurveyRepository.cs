using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Domain.SurveyAggregate;

namespace SurveyManager.Infrastructure.Persistence;

public class SurveyRepository : ISurveyRepository
{
    private static readonly List<Survey> _surveys = new();
    public void AddSurvey(Survey survey)
    {
        _surveys.Add(survey);
    }

    public Survey GetSurvey(Guid id)
    {
        return _surveys.FirstOrDefault(survey => survey.Id.Value == id);
    }
}