using Mediator;

namespace Logic;
public readonly record struct OrderRequest(int Id, string ProductName, int Amount, double Price) : IRequest<OrderedEvent>;

