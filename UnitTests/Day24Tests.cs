using AdventOfCode2020;
using NUnit.Framework;
using System.Collections.Generic;
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
            Assert.AreEqual(new Coord(0, 0, 0), d);
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


        [Test]
        public void CoordEquals()
        {
            Assert.AreEqual(new Coord(1, 2, 3), new Coord(1, 2, 3));
        }

        [Test]
        public void CoordNotEquals()
        {
            Assert.AreNotEqual(new Coord(1, 2, 3), new Coord(1, 2, 4));
        }

        [Test]
        public void CoordsInHashSet()
        {
            var hs = new HashSet<Coord>();
            hs.Add(new Coord(1, 2, 3));
            hs.Add((1, 2, 3));
            Assert.AreEqual(1, hs.Count);
            Assert.IsTrue(hs.Contains((1, 2, 3)));
            hs.Remove((1, 2, 3));
            Assert.AreEqual(0, hs.Count);
            Assert.IsFalse(hs.Contains(new Coord(1, 2, 3)));
        }

        [Test]
        public void AddCoord()
        {
            var c = new Coord(1, 2, 3) + (4, 5, 6);
            Assert.AreEqual(c, new Coord(5, 7, 9));
        }

        [Test]
        public void CoordIndexer()
        {
            var c = new Coord(4, 5, 6);
            Assert.AreEqual(4, c[0]);
            Assert.AreEqual(5, c[1]);
            Assert.AreEqual(6, c[2]);
        }
    }
}