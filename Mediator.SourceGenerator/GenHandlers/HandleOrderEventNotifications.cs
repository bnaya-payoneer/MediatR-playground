using Bnaya.Samples.GenMediatorSample.Logic;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Bnaya.Samples.GenMediatorSample.Handlers;
public partial class HandleOrderEventNotifications : INotificationHandler<OrderedEvent>
{
    private readonly ILogger<HandleOrderEventNotifications> _logger;

    public HandleOrderEventNotifications(ILogger<HandleOrderEventNotifications> logger)
    {
        _logger = logger;
    }

    ValueTask INotificationHandler<OrderedEvent>.Handle(OrderedEvent notification, CancellationToken cancellationToken)
    {
        _logger.Ordered(notification);
        return ValueTask.CompletedTask;
    }
}
