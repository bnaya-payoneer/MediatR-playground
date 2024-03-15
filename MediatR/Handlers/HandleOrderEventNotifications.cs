using Logic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handlers;
public partial class HandleOrderEventNotifications : INotificationHandler<OrderedEvent>
{
    private readonly ILogger<HandleOrderEventNotifications> _logger;

    public HandleOrderEventNotifications(ILogger<HandleOrderEventNotifications> logger)
    {
        _logger = logger;
    }

    Task INotificationHandler<OrderedEvent>.Handle(OrderedEvent notification, CancellationToken cancellationToken)
    {
        _logger.Ordered(notification);
        return Task.CompletedTask;
    }
}
