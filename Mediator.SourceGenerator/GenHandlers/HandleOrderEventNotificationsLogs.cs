using Bnaya.Samples.GenMediatorSample.Logic;
using Microsoft.Extensions.Logging;

namespace Bnaya.Samples.GenMediatorSample.Handlers;

public static partial class HandleOrderEventNotificationsLogs
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Order")]
    public static partial void Ordered(this ILogger logger, [LogProperties] OrderedEvent notification);
}
