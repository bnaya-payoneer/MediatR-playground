using Mediator;

namespace Bnaya.Samples.GenMediatorSample.Logic;
public readonly record struct OrderRequest(int Id, string ProductName, int Amount, double Price) : IRequest<OrderedEvent>;

