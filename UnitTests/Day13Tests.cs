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
            Assert.AreEqual(("295", ""), answer);
        }
    }
}