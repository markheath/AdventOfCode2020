using AdventOfCode2020;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day15Tests
    {
        [TestCase("0,3,6", 436)]
        [TestCase("2,1,3", 10)]
        [TestCase("1,3,2", 1)]
        [TestCase("1,2,3", 27)]
        [TestCase("2,3,1", 78)]
        [TestCase("3,2,1", 438)]
        [TestCase("3,1,2", 1836)]
        public void SolvePart1(string input, int expected)
        {
            var answer = Day15.Solve(input.Split(",").Select(int.Parse).ToList(), 2020);
            Assert.AreEqual(expected, answer);
        }

        [TestCase("0,3,6", 175594)]
        [TestCase("2,1,3", 3544142)]
        [TestCase("1,3,2", 2578)]
        [TestCase("1,2,3", 261214)]
        [TestCase("2,3,1", 6895259)]
        [TestCase("3,2,1", 18)]
        [TestCase("3,1,2", 362)]
        public void SolvePart2(string input, int expected)
        {
            var answer = Day15.Solve(input.Split(",").Select(int.Parse).ToList(), 30000000);
            Assert.AreEqual(expected, answer);
        }
    }
}