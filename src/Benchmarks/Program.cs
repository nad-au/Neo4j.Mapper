using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance);

            config.Add(new CategoriesColumn());
            BenchmarkRunner.Run<MapperComparisonBenchmarks>(config);
            //BenchmarkRunner.Run<MapperComparisonBenchmarks>(new DebugInProcessConfig());
        }
    }
}
