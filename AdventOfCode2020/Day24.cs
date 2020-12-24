using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day24 : ISolver
    {
        public (string, string) ExpectedResult => ("244", "");

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
            return (part1.ToString(), "");
        }
    }
}
