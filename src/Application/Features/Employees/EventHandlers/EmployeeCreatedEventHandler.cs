using HrmBaharu.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HrmBaharu.Application.Features.Employees.EventHandlers;

public class EmployeeCreatedEventHandler : INotificationHandler<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedEventHandler> _logger;

    public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HrmBaharu Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
