using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day23Tests
    {
        [Test]
        public void CanParseCups()
        {
            var c = Day23.Parse("389125467");
            Assert.AreEqual("389125467", Day23.Describe(c));
        }

        [Test]
        public void CanDoFirstMove()
        {
            //var testInput = @"".Split("\r\n");
            //var answer = new Day23().Solve(testInput);
            //Assert.AreEqual(("a", "b"), answer);
            var c = Day23.Parse("389125467");
            c = Day23.MoveOne(c, 9);
            Assert.AreEqual(2, c);
            Assert.AreEqual("289154673", Day23.Describe(c));
        }

        [Test]
        public void CanDo10Moves()
        {
            var c = Day23.Parse("389125467");
            c = Day23.Move(c, 10);
            Assert.AreEqual(8, c);
            Assert.AreEqual("837419265", Day23.Describe(c));
        }

        [Test]
        public void SolveAfter100Moves()
        {
            var answer = Day23.Part1("389125467");
            Assert.AreEqual("67384529", answer);
        }
        [Test]
        public void SolvePart2Slow()
        {
            var answer = Day23.Part2("389125467");
            Assert.AreEqual("149245887792", answer);
        }

    }
}