using Bnaya.Samples.MediateRSample.Handlers;
using Bnaya.Samples.MediateRSample.Logic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(OrderedEvent).Assembly,
        typeof(OrderHandler).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/", async (OrderRequest request, [FromServices] IMediator mediator) =>
{
    OrderedEvent orderedEvent = await mediator.Send(request);
    return orderedEvent;
})
.WithOpenApi();

app.Run();

