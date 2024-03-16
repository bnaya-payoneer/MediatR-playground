using Bnaya.Samples.GenMediatorSample.Logic;
using Mediator;

namespace Bnaya.Samples.GenMediatorSample.Handlers;



public class OrderHandler : IRequestHandler<OrderRequest, OrderedEvent>
{
    private readonly IMediator _mediator;
    private readonly TimeProvider _timeProvider;

    public OrderHandler(
        IMediator mediator,
        TimeProvider? timeProvider = null)
    {
        _mediator = mediator;
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    async ValueTask<OrderedEvent> IRequestHandler<OrderRequest, OrderedEvent>.Handle(
        OrderRequest request,
        CancellationToken cancellationToken)
    {
        var e = new OrderedEvent(request.Id, _timeProvider.GetUtcNow());
        await _mediator.Publish(e);
        return e;
    }
}
