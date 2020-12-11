using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    

    public class Day11 : ISolver
    {
        public (string, string) ExpectedResult => ("2354", "");

        public (string, string) Solve(string[] input)
        {
            var iterations = 0;
            var map = input;
            var mutated = false;
            do
            {
                (mutated, map) = Mutate(map);
                iterations++;
            } while (mutated);
            var part1 = map.SelectMany(r => r).Count(c => c == '#');
            return (part1.ToString(), "");
        }

        public static int CountAdjacent(string[] map, int row, int col)
        {
            var x = from r in Enumerable.Range(row - 1, 3)
            from c in Enumerable.Range(col - 1, 3)
            where r >= 0 && c >= 0 && r < map.Length && c < map[0].Length
            where !(r == row && c == col)
            where map[r][c] == '#'
            select 1;
            return x.Sum();
        }

        public static (bool, string[]) Mutate(string[] map)
        {
            bool mutated = false;

            string[] output = new string[map.Length];
            for(var r = 0; r < map.Length; r++)
            {
                var sb = new StringBuilder();
                for(var c = 0; c < map[r].Length; c++)
                {
                    var adjacent = CountAdjacent(map, r, c);
                    if (map[r][c] == 'L' && adjacent == 0)
                    {
                        sb.Append('#');
                        mutated = true;
                    }
                    else if (map[r][c] == '#' && adjacent >= 4)
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
