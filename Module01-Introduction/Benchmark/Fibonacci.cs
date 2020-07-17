namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    using System.Collections.Generic;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Validators;
    
    [MemoryDiagnoser]
    [MarkdownExporter]
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            var cache = new Dictionary<ulong, ulong>
            {
                [1] = 1,
                [2] = 1,
            };

            ulong Compute(ulong n)
            {
                if (cache.ContainsKey(n))
                {
                    return cache[n];
                }

                var result = Compute(n - 2) + Compute(n - 1);
                cache[n] = result;
                return result;
            }

            return Compute(n);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            ulong n1 = 1;
            ulong n2 = 1;

            for (ulong i = 3; i <= n; i++)
            {
                ulong sum = n1 + n2;
                n1 = n2;
                n2 = sum;
            }

            return n2;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
