using System;
using static MoreLinq.Extensions.SplitExtension;
using static MoreLinq.Extensions.CartesianExtension;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day20 : ISolver
    {
        public (string, string) ExpectedResult => ("79412832860579", "");

        public class Tile
        {
            public enum Side
            {
                Top,
                Right,
                Bottom,
                Left
            }
            public Dictionary<Side, (Tile,bool)> Connected { get; } = new Dictionary<Side,(Tile,bool)>();
            public Dictionary<Side, (string,string)> Matchers { get; } = new Dictionary<Side, (string,string)>();
            public Tile(IEnumerable<string> input)
            {
                int Int(string s) => int.Parse(Regex.Match(s, "\\d+").Value);
                Id = Int(input.First());
                Pattern = input.Skip(1).ToArray();
                Matchers[Side.Top] = (Pattern[0], new string(Pattern[0].Reverse().ToArray()));
                Matchers[Side.Bottom] = (Pattern[Pattern.Length - 1],new string(Pattern[Pattern.Length - 1].Reverse().ToArray()));
                var left = new string(Pattern.Select(s => s[0]).ToArray());
                Matchers[Side.Left] = (left, new string(left.Reverse().ToArray()));
                var right = new string(Pattern.Select(s => s.Last()).ToArray());
                Matchers[Side.Right] = (right, new string(right.Reverse().ToArray()));
            }
            public int Id { get; }
            public string[] Pattern { get; }
           
            public IEnumerable<(Side,string,bool)> AvailableSides()
            {
                foreach (var side in Enum.GetValues(typeof(Side)).Cast<Side>().Where(s => !Connected.ContainsKey(s)))
                {
                    var (forward, rev) = Matchers[side];
                    yield return (side, forward, false);
                    yield return (side, rev, true);
                }
            }
            
            public bool IsCorner()
            {
                return (!Connected.ContainsKey(Side.Top) || !Connected.ContainsKey(Side.Bottom)) &&
                    (!Connected.ContainsKey(Side.Left) || !Connected.ContainsKey(Side.Right));
            }
            public void Connect(Side side, Tile tile, bool reverse)
            {
                if (Connected.ContainsKey(side))
                    throw new InvalidOperationException("Already connected");
                Connected[side] = (tile,reverse);
            }

            public void Match(Tile other)
            {
                foreach(var pair in AvailableSides().Cartesian(other.AvailableSides(), (a,b) => 
                    new { a,b }))
                {
                    if (pair.a.Item2 == pair.b.Item2)
                    {
                        Connect(pair.a.Item1, other, pair.a.Item3);
                        other.Connect(pair.b.Item1, this, pair.b.Item3);
                        break;
                    }
                }
            }
        }

        public (string, string) Solve(string[] input)
        {
            var tiles = input.Split("").Select(t => new Tile(t)).ToArray();
            var part1 = SolvePart1(tiles).ToString();
            return (part1.ToString(),"");
        }
        public static long SolvePart1(Tile[] tiles)
        {
            // corners have 2 adjacent sides with no matches
            var corners = new List<Tile>();
            for (var t = 0; t < tiles.Length; t++)
            {
                for (var k = t + 1; k < tiles.Length; k++)
                {
                    tiles[t].Match(tiles[k]);
                }
                if (tiles[t].IsCorner())
                {
                    corners.Add(tiles[t]);
                    if (corners.Count == 4)
                    {
                        return corners.Select(t => (long)t.Id).Aggregate((a, b) => a * b);
                    }
                }
            }

            throw new InvalidOperationException("not enough corners found");
        }

    }
}
