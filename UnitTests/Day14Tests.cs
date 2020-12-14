using AdventOfCode2020;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    public class Day14Tests
    {
        [Test]
        public void SolvePart1WithTestInput()
        {
            var testInput = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0".Split("\r\n");
            var answer = Day14.SolvePart1(testInput);
            Assert.AreEqual(165, answer);
        }

        [Test]
        public void SolvePart2WithTestInput()
        {
            var testInput = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1".Split("\r\n");
            var answer = Day14.SolvePart2(testInput);
            Assert.AreEqual(208, answer);
        }

        [Test]
        public void GetLocations2FloatingBits()
        {
            var mask = @"000000000000000000000000000000X1001X";
            var answer = Day14.GetLocations(mask, 42).ToArray();
            Assert.AreEqual("26,27,58,59", String.Join(",", answer));
        }

        [Test]
        public void GetLocations3FloatingBits()
        {
            var mask = @"00000000000000000000000000000000X0XX";
            var answer = Day14.GetLocations(mask, 26).ToArray();
            Assert.AreEqual("16,17,18,19,24,25,26,27", String.Join(",", answer));
        }
    }
}