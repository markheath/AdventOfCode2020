using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day11Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL".Split("\r\n");
            var answer = new Day11().Solve(testInput);
            Assert.AreEqual(("a","b"), answer);
        }
    }
}