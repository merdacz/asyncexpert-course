namespace Benchmark
{
    using System.Collections.Generic;
    using Dotnetos.AsyncExpert.Homework.Module01.Benchmark;
    using Xunit;

    public class FibTests
    {
        private FibonacciCalc sut;

        public FibTests()
        {
            sut = new FibonacciCalc();
        }

        [Theory]
        [MemberData("Data")]
        public void Recursive(ulong n, ulong expected)
        {
            ulong actual = sut.Recursive(n);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("Data")]
        public void RecursiveWithMemo(ulong n, ulong expected)
        {
            ulong actual = sut.RecursiveWithMemoization(n);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("Data")]
        public void Iterative(ulong n, ulong expected)
        {
            ulong actual = sut.Iterative(n);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, 1 },
                new object[] { 2, 1 },
                new object[] { 3, 2 },
                new object[] { 4, 3 },
                new object[] { 5, 5 },
                new object[] { 6, 8 },
                new object[] { 7, 13 },
            };
    }
}
