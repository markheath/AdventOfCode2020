using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day14Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0".Split("\r\n");
            var answer = new Day14().Solve(testInput);
            Assert.AreEqual(("165", ""), answer);
        }

    }
}