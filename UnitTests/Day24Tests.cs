using AdventOfCode2020;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day24Tests
    {
        [Test]
        public void CanParseDirections()
        {
            var d = Day24.ParseDir("neeenesenwnww").ToArray();
            Assert.AreEqual(new[] { "ne", "e", "e", "ne", "se", "nw", "nw", "w" }, d);
        }

        [Test]
        public void FollowPath()
        {
            var d = Day24.FollowPath("nwwswee");
            Assert.AreEqual((0, 0, 0), d);
        }

        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew".Split("\r\n");
            var answer = new Day24().Solve(testInput);
            Assert.AreEqual(("10", "2208"), answer);
        }
    }
}