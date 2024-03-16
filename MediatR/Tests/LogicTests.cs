using Bnaya.Samples.MediateRSample.Logic;
using Cocona;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bnaya.Samples.MediateRSample.Tests;

public class LogicTests
{
    private readonly IMediator _mediator;
    private readonly ILoggerProvider _loggerProvider = A.Fake<ILoggerProvider>();
    private readonly ILogger _logger = A.Fake<ILogger>();
    private readonly ITestOutputHelper _testOutput;
    private readonly TimeProvider _timeProvider = A.Fake<TimeProvider>();

    public LogicTests(ITestOutputHelper testOutput)
    {
        A.CallTo(() => _loggerProvider.CreateLogger(A<string>._)).ReturnsLazily((string categoryName) => _logger);
        A.CallTo(() => _timeProvider.GetUtcNow()).Returns(DateTimeOffset.UtcNow);

        A.CallTo(() => _timeProvider.CreateTimer(A<TimerCallback>._, A<object?>._, A<TimeSpan>._, A<TimeSpan>._)).ReturnsLazily((TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) =>
        {
            return GetImmediateTimer(callback, state);
        });

        var builder = CoconaApp.CreateBuilder();
        var services = builder.Services;
        services.AddSingleton(_timeProvider);
        services.AddLogging(cfg =>
        {
            cfg.ClearProviders();
            cfg.AddProvider(_loggerProvider);
            cfg.AddJsonConsole();
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Handlers.OrderHandler).Assembly);
        });
        var app = builder.Build();
        IServiceProvider serviceProvider = app.Services;
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _testOutput = testOutput;
    }

    [Fact]
    public async Task OrderRequestTest()
    {
        OrderRequest request = new OrderRequest(10, "Product", 5, 100.0);
        OrderedEvent response = await _mediator.Send(request);
        Assert.Equal(request.Id, response.Id);
        A.CallTo(() => _logger.Log(
                       LogLevel.Information,
                       A<EventId>.Ignored,
                       A<LoggerMessageState>.Ignored,
                       null,
                       A<Func<LoggerMessageState, Exception, string>>.Ignored))
            .MustHaveHappenedOnceExactly();
    }

    private ITimer GetImmediateTimer(TimerCallback callback, object? state)
    {
        var timer = A.Fake<ITimer>();
        _ = Task.Run(() => callback(state));
        return timer;
    }

    [Fact]
    public async Task OrderStreamTest()
    {
        var request = new OrderStreamRequest(999, "Product");
        IAsyncEnumerable<OrderRequest> items = _mediator.CreateStream(request);

        int i = 0;
        await foreach (var item in items)
        {
            Assert.Equal(request.Id, item.Id);
            Assert.Equal(request.ProductName, item.ProductName);
            Assert.Equal(i % 3 + 1, item.Amount);
            Assert.Equal(100 + (i % 6), item.Price);
            i++;
        }
    }

    [Fact]
    public async Task OrderNotificationTest()
    {
        OrderedEvent e = new(10, DateTimeOffset.UtcNow);
        await _mediator.Publish(e);
        A.CallTo(() => _logger.Log(
                       LogLevel.Information,
                       A<EventId>.Ignored,
                       A<LoggerMessageState>.Ignored,
                       null,
                       A<Func<LoggerMessageState, Exception, string>>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
}