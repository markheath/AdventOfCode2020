﻿using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day14Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"".Split("\r\n");
            var answer = new Day14().Solve(testInput);
            Assert.AreEqual(("a", "b"), answer);
        }
    }
}