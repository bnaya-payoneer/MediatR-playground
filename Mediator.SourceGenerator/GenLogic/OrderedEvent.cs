using Mediator;

namespace Bnaya.Samples.GenMediatorSample.Logic;

public readonly record struct OrderedEvent(int Id, DateTimeOffset OrderedAt) : INotification;

