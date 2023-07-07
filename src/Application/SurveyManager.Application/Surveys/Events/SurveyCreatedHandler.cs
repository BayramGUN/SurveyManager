using MediatR;

using Microsoft.Extensions.Logging;

using SurveyManager.Domain.SurveyAggregate.Events;

namespace SurveyManager.Application.Surveys.Events;

public class SurveyCreatedHandler : INotificationHandler<SurveyCreated>
{
    private readonly ILogger<SurveyCreatedHandler> _logger;

    public SurveyCreatedHandler(
        ILogger<SurveyCreatedHandler> logger
        )
    {
        _logger = logger;
    }

    public async Task Handle(SurveyCreated notification, CancellationToken cancellationToken)
    {
        var url = $"http://127.0.0.1:5500/surveyPage.html?={notification.Survey.Id.Value.ToString()}";
        _logger.LogInformation(url);
        await Task.Delay(2000, cancellationToken);
    }
}