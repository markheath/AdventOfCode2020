using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day16Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12".Split("\r\n");
            var answer = new Day16().Solve(testInput);
            Assert.AreEqual(("71", ""), answer);
        }
    }
}