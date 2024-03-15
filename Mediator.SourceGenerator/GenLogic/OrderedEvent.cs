using Mediator;

namespace Logic;

public readonly record struct OrderedEvent(int Id, DateTimeOffset OrderedAt) : INotification;

