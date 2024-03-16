using MediatR;

namespace Bnaya.Samples.MediateRSample.Logic;

public readonly record struct OrderStreamRequest(int Id, string ProductName) : IStreamRequest<OrderRequest> { }

