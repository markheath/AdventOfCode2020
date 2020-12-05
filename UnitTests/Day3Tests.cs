using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day3Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#".Split("\r\n");
            var solver = new Day3();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("7", "336"), solution);
        }
    }


}