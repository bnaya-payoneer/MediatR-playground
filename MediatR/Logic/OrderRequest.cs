using MediatR;

namespace Bnaya.Samples.MediateRSample.Logic;
public readonly record struct OrderRequest(int Id, string ProductName, int Amount, double Price) : IRequest<OrderedEvent>;

