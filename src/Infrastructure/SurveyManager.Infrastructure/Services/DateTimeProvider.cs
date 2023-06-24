using SurveyManager.Application.Common.Interfaces.Services;

namespace SurveyManager.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}