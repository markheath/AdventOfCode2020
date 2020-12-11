using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day19Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"".Split("\r\n");
            var answer = new Day19().Solve(testInput);
            Assert.AreEqual(("a", "b"), answer);
        }
    }
}