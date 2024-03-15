using Logic;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
