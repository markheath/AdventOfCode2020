using MoreLinq;
using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day11Extensions
    { 
        public static char? Get(this string[] map, (int, int) pos)
        {
            var (r, c) = pos;
            if (r < 0 || c < 0 || r >= map.Length || c >= map[r].Length)
            {
                return null;
            }
            return map[r][c];
        }
        public static (int X, int Y) Add(this (int X, int Y) a, (int X, int Y) b)
            => (a.X + b.X, a.Y + b.Y);
    }


    public class Day11 : ISolver
    {

        public (string, string) ExpectedResult => ("2354", "2072");

        public (string, string) Solve(string[] input)
        {
            var part1 = Solve(input, 4, CountAdjacent1);
            var part2 = Solve(input, 5, CountAdjacent2);
            return (part1.ToString(), part2.ToString());
        }

        private static int Solve(string[] map, int occupiedThreshold,
            Func<string[],(int,int),int> countAdjacent)
        {
            var iterations = 0;
            var mutated = false;
            do
            {
                (mutated, map) = Mutate(map, occupiedThreshold, countAdjacent);
                iterations++;
            } while (mutated);
            return map.SelectMany(r => r).Count(c => c == '#');
        }

        private static readonly (int,int)[] directions = new[]
        {
            (-1,-1), (-1,0), (-1,1),
            (0,-1), (0,1),
            (1,-1), (1,0), (1,1)
        };
        public static int CountAdjacent1(string[] map, (int,int) coords)
        {

            return directions
                .Select(d => d.Add(coords))
                .Count(p => map.Get(p) == '#');
        }

        public static int CountAdjacent2(string[] map, (int,int) coords)
        {
            return directions.
                Select(d =>
                MoreEnumerable.Generate(coords, 
                    p => d.Add(p))
                    .Skip(1) // ignore initial position
                    .Select(p => map.Get(p))
                    .SkipWhile(c => c == '.')
                    .First())
                .Count(x => x == '#');
        }

        public static (bool, string[]) Mutate(string[] map, int occupiedThreshold,
            Func<string[],(int,int),int> countAdjacent)
        {
            bool mutated = false;

            string[] output = new string[map.Length];
            for(var r = 0; r < map.Length; r++)
            {
                var sb = new StringBuilder();
                for(var c = 0; c < map[r].Length; c++)
                {
                    var adjacent = countAdjacent(map, (r, c));
                    if (map[r][c] == 'L' && adjacent == 0)
                    {
                        sb.Append('#');
                        mutated = true;
                    }
                    else if (map[r][c] == '#' && adjacent >= occupiedThreshold)
                    {
                        sb.Append('L');
                        mutated = true;
                    }
                    else
                    {
                        sb.Append(map[r][c]);
                    }
                }
                output[r] = sb.ToString();            
            }
            return (mutated, output);
        }
    }
}
