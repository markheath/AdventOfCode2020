using AdventOfCode2020;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day9Tests
    {
        [Test]
        public void SolvePart1WithTestInput()
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

        [Test]
        public void SolvePart2WithTestInput()
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
            var answer = Day9.Solve2(testInput.Select(long.Parse).ToList(), 127);
            Assert.AreEqual(62, answer);
        }

    }
}