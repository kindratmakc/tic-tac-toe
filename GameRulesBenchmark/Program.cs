using BenchmarkDotNet.Running;

namespace GameRulesBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(MinimaxBenchmark));
        }
    }
}