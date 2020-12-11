using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day22Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"".Split("\r\n");
            var answer = new Day22().Solve(testInput);
            Assert.AreEqual(("a", "b"), answer);
        }
    }
}