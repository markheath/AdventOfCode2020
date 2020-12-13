using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day13Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"939
7,13,x,x,59,x,31,19".Split("\r\n");
            var answer = new Day13().Solve(testInput);
            Assert.AreEqual(("295", "1068781"), answer);
        }

        [TestCase("17,x,13,19", 3417)]
        [TestCase("67,7,59,61", 754018)]
        public void Part2(string input, long expected)
        {
            var answer = Day13.Solve2Optimized(input);
            Assert.AreEqual(expected, answer);
        }
    }
}