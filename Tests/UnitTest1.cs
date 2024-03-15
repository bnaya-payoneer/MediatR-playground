using MediatR;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Logic;

namespace Tests;

public class LogicTests
{
    private readonly IMediator _mediator;
    public LogicTests()
    {
        var builder = CoconaApp.CreateBuilder();
        var services = builder.Services;
        services.AddSingleton(TimeProvider.System);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Handlers.OrderHandler).Assembly);
        });
        var app = builder.Build();
        IServiceProvider serviceProvider = app.Services;
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    
    }

    [Fact]
    public async Task OrderTest()
    {
        OrderRequest request = new OrderRequest(10, "Product", 5, 100.0);
        OrderedEvent response = await _mediator.Send(request);
        Assert.Equal(request.Id, response.Id);
    }
}