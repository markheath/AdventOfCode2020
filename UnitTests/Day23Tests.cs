using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day23Tests
    {
        [Test]
        public void CanParseCups()
        {
            var c = Day23.Cup.Parse("389125467");
            Assert.AreEqual(3, c.Id);
            Assert.AreEqual(8, c.Next.Id);
            Assert.AreEqual(9, c.Next.Next.Id);
            Assert.AreEqual("389125467", c.ToString());
        }

        [Test]
        public void CanDoFirstMove()
        {
            //var testInput = @"".Split("\r\n");
            //var answer = new Day23().Solve(testInput);
            //Assert.AreEqual(("a", "b"), answer);
            var c = Day23.Cup.Parse("389125467");
            c = c.MoveOne(9);
            Assert.AreEqual(2, c.Id);
            Assert.AreEqual("289154673", c.ToString());
        }

        [Test]
        public void CanDo10Moves()
        {
            var c = Day23.Cup.Parse("389125467");
            c = c.Move(10);
            Assert.AreEqual(8, c.Id);
            Assert.AreEqual("837419265", c.ToString());
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