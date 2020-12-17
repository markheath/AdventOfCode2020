using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day17Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @".#.
..#
###".Split("\r\n");
            var cube = new ConwayCube(testInput);
            for (var n = 0; n < 6; n++) cube = cube.Mutate(3);
            var part1 = cube.ActiveCells;

            Assert.AreEqual(112, part1);
        }

        [Test]
        public void Solve4DWithTestInput()
        {
            var testInput = @".#.
..#
###".Split("\r\n");
            var cube = new ConwayCube(testInput);
            for (var n = 0; n < 6; n++) cube = cube.Mutate(4);
            var part2 = cube.ActiveCells;

            Assert.AreEqual(848, part2);
        }
    }
}