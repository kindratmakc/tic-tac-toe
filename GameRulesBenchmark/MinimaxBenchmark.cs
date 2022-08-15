using BenchmarkDotNet.Attributes;
using GameRules;

namespace GameRulesBenchmark;

[MemoryDiagnoser]
public class MinimaxBenchmark
{
    private Minimax _minimax = new();

    [Benchmark]
    public List<ScoredMove> Score() => _minimax.ScoreBoard(new Game(), true).ToList();

}