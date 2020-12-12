using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day12Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"F10
N3
F7
R90
F11".Split("\r\n");
            var answer = new Day12().Solve(testInput);
            Assert.AreEqual(("25", ""), answer);
        }
    }
}