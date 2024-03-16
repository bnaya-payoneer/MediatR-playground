using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<Benchmarks>();
Console.WriteLine(summary);
