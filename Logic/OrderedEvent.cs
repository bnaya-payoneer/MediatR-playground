using MediatR;

namespace Logic;

public readonly record struct OrderedEvent(int Id, DateTimeOffset OrderedAt): INotification;

