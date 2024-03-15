using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Logic;
public readonly record struct OrderRequest(int Id, string ProductName, int Amount, double Price): IRequest<OrderedEvent>;

