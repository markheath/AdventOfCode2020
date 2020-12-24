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
        private static readonly Dictionary<string, (int, int, int)> lookup = new Dictionary<string, (int, int, int)> {
            { "nw",  (0, 1, -1) },
            { "se",  (0, -1, 1) },
            { "w", (-1, 1, 0) },
            { "e", (1, -1, 0) },
            { "ne", (1, 0, -1) },
            { "sw", (-1, 0, 1) }
        };

        public HashSet<(int,int,int)> Mutate(HashSet<(int,int,int)> state)
        {
            var minx = state.Min(p => p.Item1) - 1;
            var maxx = state.Max(p => p.Item1) + 1;
            var miny = state.Min(p => p.Item2) - 1;
            var maxy = state.Max(p => p.Item2) + 1;
            var minz = state.Min(p => p.Item3) - 1;
            var maxz = state.Max(p => p.Item3) + 1;
            var newState = new HashSet<(int, int, int)>();
            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                    for (int z = minz; z <= maxz; z++)
                    {
                        var isBlack = state.Contains((x, y, z));
                        var adjacentBlack = lookup.Values.Select(d => (x + d.Item1, y + d.Item2, z + d.Item3)).Count(p => state.Contains(p));
                        // Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
                        if (isBlack)
                        {
                            if (adjacentBlack == 0 || adjacentBlack > 2)
                            {
                                // flips to black
                            }
                            else
                            {
                                // stays black
                                newState.Add((x, y, z));
                            }
                            // else flips to white - don't include
                        }
                        else
                        {
                            //Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
                            if (adjacentBlack == 2)
                            {
                                newState.Add((x, y, z));
                            }
                        }
                    }
            return newState;
        }

        public static (int,int,int) FollowPath(string path)
        {
            var pos = (0, 0, 0);
            foreach(var (x,y,z) in ParseDir(path).Select(d => lookup[d]))
            {
                pos = (pos.Item1 + x, pos.Item2 + y, pos.Item3 + z);
            }
            return pos;
        }

        // e, se, sw, w, nw, and ne
        public static IEnumerable<string> ParseDir(string input)
        {
            for(var n =0; n < input.Length; n++)
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
            HashSet<(int, int, int)> blackTiles = new HashSet<(int, int, int)>();
            foreach(var line in input)
            {
                var pos = FollowPath(line);
                if (blackTiles.Contains(pos)) 
                    blackTiles.Remove(pos); 
                else 
                    blackTiles.Add(pos);
            }
            var part1 = blackTiles.Count;

            for(int n = 0; n < 100; n++)
            {
                blackTiles = Mutate(blackTiles);
            }

            return (part1.ToString(), blackTiles.Count().ToString());
        }
    }
}
