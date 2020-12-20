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
            
            public List<Tile> TopMatches { get; } = new List<Tile>();
            public List<Tile> LeftMatches { get; } = new List<Tile>();
            public List<Tile> BottomMatches { get; } = new List<Tile>();
            public List<Tile> RightMatches { get; } = new List<Tile>();
            public bool IsComplete { get; private set; }

            public void Complete()
            {
                IsComplete = true;
            }
            public bool IsCorner()
            {
                return (TopMatches.Count == 0 || BottomMatches.Count == 0) &&
                    (LeftMatches.Count == 0 || RightMatches.Count == 0);
            }

            public void Match(Tile other)
            {
                // Tops match
                if (TopEdge == other.TopEdge ||
                    TopEdgeRev == other.TopEdge)
                {
                    TopMatches.Add(other);
                    other.TopMatches.Add(this);
                }
                if (TopEdge == other.BottomEdge ||
                    TopEdgeRev == other.BottomEdge)
                {
                    TopMatches.Add(other);
                    other.BottomMatches.Add(this);
                }
                if (TopEdge == other.RightEdge ||
                    TopEdgeRev == other.RightEdge)
                {
                    TopMatches.Add(other);
                    other.RightMatches.Add(this);
                }
                if (TopEdge == other.LeftEdge ||
                    TopEdgeRev == other.LeftEdge)
                {
                    TopMatches.Add(other);
                    other.LeftMatches.Add(this);
                }

                if (BottomEdge == other.TopEdge ||
                    BottomEdgeRev == other.TopEdge)
                {
                    BottomMatches.Add(other);
                    other.TopMatches.Add(this);
                }
                if (BottomEdge == other.BottomEdge ||
                    BottomEdgeRev == other.BottomEdge)
                {
                    BottomMatches.Add(other);
                    other.BottomMatches.Add(this);
                }
                if (BottomEdge == other.RightEdge ||
                    BottomEdgeRev == other.RightEdge)
                {
                    BottomMatches.Add(other);
                    other.RightMatches.Add(this);
                }
                if (BottomEdge == other.LeftEdge ||
                    BottomEdgeRev == other.LeftEdge)
                {
                    BottomMatches.Add(other);
                    other.LeftMatches.Add(this);
                }

                if (LeftEdge == other.LeftEdge ||
                    LeftEdgeRev == other.LeftEdge)
                {
                    LeftMatches.Add(other);
                    other.LeftMatches.Add(this);
                }
                if (LeftEdge == other.RightEdge ||
                    LeftEdgeRev == other.RightEdge)
                {
                    LeftMatches.Add(other);
                    other.RightMatches.Add(this);
                }
                if (LeftEdge == other.TopEdge ||
                    LeftEdgeRev == other.TopEdge)
                {
                    LeftMatches.Add(other);
                    other.TopMatches.Add(this);
                }
                if (LeftEdge == other.BottomEdge ||
                    LeftEdgeRev == other.BottomEdge)
                {
                    LeftMatches.Add(other);
                    other.BottomMatches.Add(this);
                }


                if (RightEdge == other.LeftEdge ||
                    RightEdgeRev == other.LeftEdge)
                {
                    RightMatches.Add(other);
                    other.LeftMatches.Add(this);
                }
                if (RightEdge == other.RightEdge ||
                    RightEdgeRev == other.RightEdge)
                {
                    RightMatches.Add(other);
                    other.RightMatches.Add(this);
                }
                if (RightEdge == other.TopEdge ||
                    RightEdgeRev == other.TopEdge)
                {
                    RightMatches.Add(other);
                    other.TopMatches.Add(this);
                }
                if (RightEdge == other.BottomEdge ||
                    RightEdgeRev == other.BottomEdge)
                {
                    RightMatches.Add(other);
                    other.BottomMatches.Add(this);
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
