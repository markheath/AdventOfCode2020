using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day24 : ISolver
    {
        public (string, string) ExpectedResult => ("244", "3665");

        // axes are n/s, ne/sw, nw,se
        //          e/w, ne/sw, nw,se
        private static readonly Dictionary<string, Coord> lookup = new Dictionary<string, Coord> {
            { "nw",  (0, 1, -1) },
            { "se",  (0, -1, 1) },
            { "w", (-1, 1, 0) },
            { "e", (1, -1, 0) },
            { "ne", (1, 0, -1) },
            { "sw", (-1, 0, 1) }
        };

        public HashSet<Coord> Mutate(HashSet<Coord> state)
        {
            var minx = state.Min(p => p.X) - 1;
            var maxx = state.Max(p => p.X) + 1;
            var miny = state.Min(p => p.Y) - 1;
            var maxy = state.Max(p => p.Y) + 1;
            var minz = state.Min(p => p.Z) - 1;
            var maxz = state.Max(p => p.Z) + 1;
            var newState = new HashSet<Coord>();
            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                    for (int z = minz; z <= maxz; z++)
                    {
                        var pos = new Coord(x, y, z);
                        var isBlack = state.Contains(pos);
                        var adjacentBlack = lookup.Values.Count(d => state.Contains(d+pos));
                        // Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
                        if (isBlack)
                        {
                            if (adjacentBlack == 1 || adjacentBlack == 2)
                            {
                                // stays black
                                newState.Add(pos);
                            }
                        }
                        else if (adjacentBlack == 2)
                        {
                            //Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
                            newState.Add(pos);
                        }
                    }
            return newState;
        }

        public static Coord FollowPath(string path)
        {
            var pos = new Coord(0, 0, 0);
            foreach(var move in ParseDir(path).Select(d => lookup[d]))
            {
                pos += move;
            }
            return pos;
        }

        // e, se, sw, w, nw, and ne
        public static IEnumerable<string> ParseDir(string input)
        {
            for(var n = 0; n < input.Length; n++)
            {
                if (input[n] == 'n' || input[n] == 's')
                {
                    yield return input[n..(n + 2)];
                    n++;
                }
                else
                {
                    yield return input[n..(n+1)];
                }
            }
        }

        public (string, string) Solve(string[] input)
        {
            var blackTiles = new HashSet<Coord>();
            foreach(var line in input)
            {
                var pos = FollowPath(line);
                if (!blackTiles.Remove(pos)) 
                    blackTiles.Add(pos);
            }
            var part1 = blackTiles.Count;

            for(int n = 0; n < 100; n++)
            {
                blackTiles = Mutate(blackTiles);
            }

            return (part1.ToString(), blackTiles.Count.ToString());
        }
    }
}
