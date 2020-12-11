using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day11Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL".Split("\r\n");
            var answer = new Day11().Solve(testInput);
            Assert.AreEqual(("37","26"), answer);
        }

        [Test]
        public void CountAdjacent2()
        {
            var map = @".......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....".Split("\r\n");
            var seats = Day11.CountAdjacent2(map, (4, 3));
            Assert.AreEqual(8, seats);
        }
    }
}