namespace SurveyManager.Application.Common.Services;

public interface IServiceManagement
{
    Task SendEmail(string id);
    Task UpdateDatabase();
    Task GenerateMerchandise();
    Task SyncData();
}