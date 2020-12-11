using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day20Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"".Split("\r\n");
            var answer = new Day20().Solve(testInput);
            Assert.AreEqual(("a", "b"), answer);
        }
    }
}