using MediatR;

namespace Bnaya.Samples.MediateRSample.Logic;

public readonly record struct OrderedEvent(int Id, DateTimeOffset OrderedAt) : INotification;

