using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day23Tests
    {
        [Test]
        public void CanParseCups()
        {
            var d = new Day23();
            var c = d.Parse("389125467");
            Assert.AreEqual("389125467", d.Describe(c));
        }

        [Test]
        public void CanDoFirstMove()
        {
            var d = new Day23();
            var c = d.Parse("389125467");
            c = d.MoveOne(c);
            Assert.AreEqual(2, c);
            Assert.AreEqual("289154673", d.Describe(c));
        }

        [Test]
        public void CanDo10Moves()
        {
            var d = new Day23();
            var c = d.Parse("389125467");
            c = d.Move(c, 10);
            Assert.AreEqual(8, c);
            Assert.AreEqual("837419265", d.Describe(c));
        }

        [Test]
        public void SolveAfter100Moves()
        {
            var d = new Day23();
            var answer = d.Part1("389125467");
            Assert.AreEqual("67384529", answer);
        }
        [Test]
        public void SolvePart2Slow()
        {
            var d = new Day23();
            var answer = d.Part2("389125467");
            Assert.AreEqual("149245887792", answer);
        }
    }
}