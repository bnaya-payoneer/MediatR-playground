using Mediator;

namespace Bnaya.Samples.GenMediatorSample.Logic;

public readonly record struct OrderStreamRequest(int Id, string ProductName) : IStreamRequest<OrderRequest> { }

