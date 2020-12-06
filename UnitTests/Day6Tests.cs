using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day6Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"abc

a
b
c

ab
ac

a
a
a
a

b".Split("\r\n");
            var solver = new Day6();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("11", "6"), solution);
        }
    }

}