using MediatR;

namespace Logic;

public readonly record struct OrderStreamRequest(int Id, string ProductName) : IStreamRequest<OrderRequest> { }

