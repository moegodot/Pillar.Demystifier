using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Utopia.Demystifier.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.NetCoreApp21)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [Config(typeof(Config))]
    public class ExceptionTests
    {
        [Benchmark(Baseline = true, Description = ".ToString()")]
        public string Baseline() => new Exception().ToString();

        [Benchmark(Description = "Demystify().ToString()")]
        public string Demystify() => new Exception().Demystify().ToString();

        [Benchmark(Description = "ColoredDemystify().ToString()")]
        public string ColoredDemystify() => new Exception().ColoredDemystify().ToString();

        [Benchmark(Description = "ToColoredStringDemystifiedtify()")]
        public string ToColoredString() => new Exception().ToColoredStringDemystified();

        [Benchmark(Description = "(left, right).ToString()")]
        public string ToStringForTupleBased() => GetException(() => ReturnsTuple()).ToString();

        [Benchmark(Description = "(left, right).Demystify().ToString()")]
        public string ToDemystifyForTupleBased() => GetException(() => ReturnsTuple()).Demystify().ToString();

        [Benchmark(Description = "(left, right).ColoredDemystify().ToString()")]
        public string ToColoedDemystifyForTupleBased() => GetException(() => ReturnsTuple()).ColoredDemystify().ToString();

        [Benchmark(Description = "(left, right).ToColoredStringDemystified()")]
        public string ToColoredStringForTupleBased() => GetException(() => ReturnsTuple()).ToColoredStringDemystified();

        private static Exception GetException(Action action)
        {
            try
            {
                action();
                throw new InvalidOperationException("Should not be reachable.");
            }
            catch (Exception e)
            {
                return e;
            }
        }

        private static List<(int left, int right)> ReturnsTuple() => throw new Exception();
    }
}
