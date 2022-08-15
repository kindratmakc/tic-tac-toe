using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using GameRules;

namespace GameRulesBenchmark;

[MemoryDiagnoser]
public class GameBenchmark
{
    private Game _game = new();
    private Consumer _consumer = new();

    [Benchmark]
    public void GetEmptySpots() => _game.GetEmptySpots().Consume(_consumer);
}