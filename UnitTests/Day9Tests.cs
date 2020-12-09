using AdventOfCode2020;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day9Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".Split("\r\n");
            var answer = Day9.Solve(testInput.Select(long.Parse).ToList(), 5);
            Assert.AreEqual(127, answer);
        }

    }
}