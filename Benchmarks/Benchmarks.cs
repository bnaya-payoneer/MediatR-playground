using BenchmarkDotNet.Attributes;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


// https://github.com/dotnet/BenchmarkDotNet?tab=readme-ov-file

//[SimpleJob(RuntimeMoniker.NativeAot80)]
//[SimpleJob(RuntimeMoniker.Net80)]
//[RPlotExporter]
public class Benchmarks
{
    private MediatR.IMediator _mediatR;
    private Mediator.IMediator _mediator;


    [GlobalSetup]
    public void Setup()
    {
        _mediatR = InitMediatR();
        _mediator = InitMediator();
    }


    //[Benchmark]
    [Benchmark(Baseline = true)]
    public async Task MediatRBenchmark()
    {
        var request = new Bnaya.Samples.MediateRSample.Logic.OrderRequest(10, "Product", 5, 100.0);
        await _mediatR.Send(request);
    }

    [Benchmark]
    public async Task MediatorBenchmark()
    {
        var request = new Bnaya.Samples.GenMediatorSample.Logic.OrderRequest(10, "Product", 5, 100.0);
        await _mediator.Send(request);
    }

    #region InitMediatR

    private MediatR.IMediator InitMediatR()
    {
        var builder = CoconaApp.CreateBuilder();
        var services = builder.Services;
        services.AddSingleton(TimeProvider.System);
        services.AddLogging(cfg =>
        {
            cfg.ClearProviders();
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Bnaya.Samples.MediateRSample.Handlers.OrderHandler).Assembly);
        });
        var app = builder.Build();
        IServiceProvider serviceProvider = app.Services;
        var mediator = serviceProvider.GetRequiredService<MediatR.IMediator>();
        return mediator;
    }

    #endregion // InitMediatR

    #region InitMediator

    private Mediator.IMediator InitMediator()
    {
        var builder = CoconaApp.CreateBuilder();
        var services = builder.Services;
        services.AddSingleton(TimeProvider.System);
        services.AddLogging(cfg =>
        {
            cfg.ClearProviders();
        });
        services.AddMediator();
        var app = builder.Build();
        IServiceProvider serviceProvider = app.Services;
        var mediator = serviceProvider.GetRequiredService<Mediator.IMediator>();
        return mediator;
    }

    #endregion // InitMediator
}