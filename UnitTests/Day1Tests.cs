using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day1Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"1721
                979
                366
                299
                675
                1456".Split("\r\n");
            var day1 = new Day1();
            var solution = day1.Solve(testInput);
            Assert.AreEqual(("514579", "241861950"), solution);
        }
    }
}