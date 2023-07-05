using MediatR;

using SurveyManager.Domain.SurveyAggregate.Events;

namespace SurveyManager.Application.Surveys.Events;

public class DummyHandler : INotificationHandler<SurveyCreated>
{
    public Task Handle(SurveyCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}