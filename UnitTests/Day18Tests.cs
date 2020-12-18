using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day18Tests
    {
        [TestCase("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [TestCase("2 * 3 + (4 * 5)", 26)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void SolveEquation(string equation, long expected)
        {
            var calculated = Day18.SolveEquation(equation);
            Assert.AreEqual(expected, calculated);
        }
    }
}