using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day1 : ISolver
    {
        public (string, string) ExpectedResult => ("1015476", "200878544");

        public (string, string) Solve(string[] input)
        {
            return ($"{Solve(input, 2)}", $"{Solve(input, 3)}");
        }

        long Solve(IEnumerable<string> input, int count) => input.Select(long.Parse).Subsets(count).First(n => n.Sum() == 2020).Aggregate((x, y) => x * y);
    }
}
