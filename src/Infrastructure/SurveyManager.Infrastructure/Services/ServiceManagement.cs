using System.Diagnostics;

using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Application.Common.Services;
using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;

namespace SurveyManager.Infrastructure.Services;

public class ServiceManagement : IServiceManagement
{
    private readonly ISurveyRepository _surveyRepository;

    public ServiceManagement(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task GenerateMerchandise()
    {
        Console.WriteLine($"Generated marchandise at {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}");
        await Task.CompletedTask;
    }

    public async Task SendEmail(string id)
    {
        var url = $"http://127.0.0.1:5500/surveyPage.html?={id}";
        Console.WriteLine($"{url} Created at {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}");
        await Task.CompletedTask;
    }

    public async Task SyncData()
    {
        Console.WriteLine($"Syncronized at {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}");
        await Task.CompletedTask;
    }

    public async Task UpdateDatabase()
    {
        var surveys = await _surveyRepository.GetAllSurveysAsync();
        var updatedSurveys = new List<Survey>();
        surveys.ForEach(survey =>
        {
            if (survey.ExpiryDate <= DateTime.UtcNow.AddHours(3) && survey.IsActive == true)
            {
                var updatedSurvey = survey.Update(
                    surveyId: SurveyId.Create(survey.Id.Value),
                    hostId: HostId.Create(survey.HostId.Value),
                    title: survey.Title,
                    expiryDate: survey.ExpiryDate,
                    survey.CreatedDateTime,
                    isActive: false,
                    questions: survey.Questions.ToList(),
                    description: survey.Description                
                );    
                updatedSurveys.Add(updatedSurvey);
                Console.WriteLine($"Updated at {DateTime.UtcNow.AddHours(3).ToString("yyyy-MM-dd HH:mm:ss")}");
            }
        });
        await _surveyRepository.UpdateSurveysAsync(updatedSurveys);
    }
}