using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day16Tests
    {
        [Test]
        public void SolvePart1WithTestInput()
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
            var (_,rules,tickets) = Day16.ParseInput(testInput);
            var scanningError = Day16.TotalScanningError(rules, tickets);
            Assert.AreEqual(71, scanningError);
        }

        [Test]
        public void SolvePart2WithTestInput()
        {
            var testInput = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9".Split("\r\n");
            var (_, rules, tickets) = Day16.ParseInput(testInput);
            var solution = Day16.SolvePositionRules(rules, tickets);
            Assert.AreEqual("row", solution[0].Name);
            Assert.AreEqual("class", solution[1].Name);
            Assert.AreEqual("seat", solution[2].Name);
        }
    }
}