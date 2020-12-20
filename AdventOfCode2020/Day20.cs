using System;
using static MoreLinq.Extensions.SplitExtension;
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
            private string TopEdge { get; }
            private string TopEdgeRev { get; }
            private string BottomEdge { get; }
            private string BottomEdgeRev { get; }
            private string LeftEdge { get; }
            private string LeftEdgeRev { get; }
            private string RightEdge { get; }
            private string RightEdgeRev { get; }

            public enum Side
            {
                Top,
                Right,
                Bottom,
                Left
            }
            public Dictionary<Side,Tile> Connected { get; } = new Dictionary<Side,Tile>();
            public Tile(IEnumerable<string> input)
            {
                int Int(string s) => int.Parse(Regex.Match(s, "\\d+").Value);
                Id = Int(input.First());
                Pattern = input.Skip(1).ToArray();
                TopEdge = Pattern[0];
                TopEdgeRev = new string(Pattern[0].Reverse().ToArray());
                BottomEdge = Pattern[Pattern.Length - 1];
                BottomEdgeRev = new string(Pattern[Pattern.Length - 1].Reverse().ToArray());
                LeftEdge = new string(Pattern.Select(s => s[0]).ToArray());
                LeftEdgeRev = new string(LeftEdge.Reverse().ToArray());
                RightEdge = new string(Pattern.Select(s => s.Last()).ToArray());
                RightEdgeRev = new string(RightEdge.Reverse().ToArray());
            }
            public int Id { get; }
            public string[] Pattern { get; }
           
            

            public bool IsCorner()
            {
                return (!Connected.ContainsKey(Side.Top) || !Connected.ContainsKey(Side.Bottom)) &&
                    (!Connected.ContainsKey(Side.Left) || !Connected.ContainsKey(Side.Right));
            }
            public void Connect(Side side, Tile tile)
            {
                if (Connected.ContainsKey(side))
                    throw new InvalidOperationException("Already connected");
                Connected[side] = tile;
            }

            public void Match(Tile other)
            {
                // Tops match
                if (TopEdge == other.TopEdge ||
                    TopEdgeRev == other.TopEdge)
                {
                    Connect(Side.Top, other);
                    other.Connect(Side.Top, this);
                }
                if (TopEdge == other.BottomEdge ||
                    TopEdgeRev == other.BottomEdge)
                {
                    Connect(Side.Top, other);
                    other.Connect(Side.Bottom, this);
                }
                if (TopEdge == other.RightEdge ||
                    TopEdgeRev == other.RightEdge)
                {
                    Connect(Side.Top, other);
                    other.Connect(Side.Right, this);
                }
                if (TopEdge == other.LeftEdge ||
                    TopEdgeRev == other.LeftEdge)
                {
                    Connect(Side.Top, other);
                    other.Connect(Side.Left, this);
                }

                if (BottomEdge == other.TopEdge ||
                    BottomEdgeRev == other.TopEdge)
                {
                    Connect(Side.Bottom, other);
                    other.Connect(Side.Top, this);
                }
                if (BottomEdge == other.BottomEdge ||
                    BottomEdgeRev == other.BottomEdge)
                {
                    Connect(Side.Bottom, other);
                    other.Connect(Side.Bottom, this);
                }
                if (BottomEdge == other.RightEdge ||
                    BottomEdgeRev == other.RightEdge)
                {
                    Connect(Side.Bottom, other);
                    other.Connect(Side.Right, this);
                }
                if (BottomEdge == other.LeftEdge ||
                    BottomEdgeRev == other.LeftEdge)
                {
                    Connect(Side.Bottom, other);
                    other.Connect(Side.Left, this);
                }

                if (LeftEdge == other.LeftEdge ||
                    LeftEdgeRev == other.LeftEdge)
                {
                    Connect(Side.Left, other);
                    other.Connect(Side.Left, this);
                }
                if (LeftEdge == other.RightEdge ||
                    LeftEdgeRev == other.RightEdge)
                {
                    Connect(Side.Left, other);
                    other.Connect(Side.Right, this);
                }
                if (LeftEdge == other.TopEdge ||
                    LeftEdgeRev == other.TopEdge)
                {
                    Connect(Side.Left, other);
                    other.Connect(Side.Top, this);
                }
                if (LeftEdge == other.BottomEdge ||
                    LeftEdgeRev == other.BottomEdge)
                {
                    Connect(Side.Left, other);
                    other.Connect(Side.Bottom, this);
                }


                if (RightEdge == other.LeftEdge ||
                    RightEdgeRev == other.LeftEdge)
                {
                    Connect(Side.Right, other);
                    other.Connect(Side.Left, this);
                }
                if (RightEdge == other.RightEdge ||
                    RightEdgeRev == other.RightEdge)
                {
                    Connect(Side.Right, other);
                    other.Connect(Side.Right, this);
                }
                if (RightEdge == other.TopEdge ||
                    RightEdgeRev == other.TopEdge)
                {
                    Connect(Side.Right, other);
                    other.Connect(Side.Top, this);
                }
                if (RightEdge == other.BottomEdge ||
                    RightEdgeRev == other.BottomEdge)
                {
                    Connect(Side.Right, other);
                    other.Connect(Side.Bottom, this);
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
