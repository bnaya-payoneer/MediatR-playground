using Bnaya.Samples.GenMediatorSample.Logic;
using Mediator;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediator();

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

