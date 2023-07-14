using SurveyManager.Application.Common.Services;

namespace SurveyManager.Infrastructure.Services;

public class ServiceManagement : IServiceManagement
{
    public async Task GenerateMerchandise()
    {
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
        await Task.CompletedTask;
    }

    public async Task UpdateDatabase()
    {
        Console.WriteLine($"Updated at {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}");
        await Task.CompletedTask;
    }
}