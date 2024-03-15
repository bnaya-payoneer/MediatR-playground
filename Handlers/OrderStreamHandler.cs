using Logic;
using MediatR;

namespace Handlers;

public class OrderStreamHandler : IStreamRequestHandler<OrderStreamRequest, OrderRequest>
{
    private readonly TimeProvider _timeProvider;

    public OrderStreamHandler(TimeProvider? timeProvider)
    {
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    async IAsyncEnumerable<OrderRequest> IStreamRequestHandler<OrderStreamRequest, OrderRequest>.Handle(
        OrderStreamRequest request, 
        CancellationToken cancellationToken)
    {
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), _timeProvider);
            yield return new OrderRequest(request.Id, request.ProductName, i % 3 + 1, 100 + (i % 6) );
        }
    }
}