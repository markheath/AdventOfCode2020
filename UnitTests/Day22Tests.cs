using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day22Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10".Split("\r\n");
            var answer = new Day22().Solve(testInput);
            Assert.AreEqual(("306", ""), answer);
        }
    }
}