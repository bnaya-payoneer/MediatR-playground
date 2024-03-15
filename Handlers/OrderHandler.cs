using Logic;
using MediatR;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;

namespace Handlers;



public class OrderHandler : IRequestHandler<OrderRequest, OrderedEvent>
{
    private readonly TimeProvider _timeProvider;

    public OrderHandler(TimeProvider? timeProvider = null)
    {
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    async Task<OrderedEvent> IRequestHandler<OrderRequest, OrderedEvent>.Handle(
        OrderRequest request, 
        CancellationToken cancellationToken)
    {
        await Task.Delay(1000);
        return new OrderedEvent(request.Id, _timeProvider.GetUtcNow());
    }
}