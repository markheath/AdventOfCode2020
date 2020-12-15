using AdventOfCode2020;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day15Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"0,3,6".Split("\r\n");
            var answer = new Day15().Solve(testInput);
            Assert.AreEqual(("436", ""), answer);
        }

        [TestCase("0,3,6", 436)]
        [TestCase("2,1,3", 10)]
        [TestCase("1,3,2", 1)]
        [TestCase("2,1,3", 10)]
        [TestCase("1,2,3", 27)]
        [TestCase("2,3,1", 78)]
        [TestCase("3,2,1", 438)]
        [TestCase("3,1,2", 1836)]
        public void SolvePart1(string input, int expected)
        {
            var answer = Day15.SolvePart1(input.Split(",").Select(int.Parse).ToList());
            Assert.AreEqual(expected, answer);
        }
    }
}