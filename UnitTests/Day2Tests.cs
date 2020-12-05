using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day2Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"1-3 a: abcde
                1-3 b: cdefg
                2-9 c: ccccccccc".Split("\r\n");
            var solver = new Day2();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("2", "1"), solution);
        }
    }
}