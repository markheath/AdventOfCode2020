using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day25Tests
    {
        [Test]
        public void FindLoopSize1()
        {
            var answer = new Day25().FindSecretLoopSize(7, 5764801);
            Assert.AreEqual(8, answer);
        }

        [Test]
        public void FindLoopSize2()
        {
            var answer = new Day25().FindSecretLoopSize(7, 17807724);
            Assert.AreEqual(11, answer);
        }

        [Test]
        public void RunWithTestInput()
        {
            var testInput = @"5764801
17807724".Split("\r\n");
            var answer = new Day25().Solve(testInput);
            Assert.AreEqual("14897079", answer.Part1);
        }
    }
}