using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day9 : ISolver
    {
        public (string, string) ExpectedResult => ("1124361034", "");

        public (string, string) Solve(string[] input)
        {
            var numbers = input.Select(i => long.Parse(i)).ToList();
            var part1 = Solve(numbers, 25);
            return (part1.ToString(), "");
        }

        public static long Solve(IList<long> numbers, int preamble)
        {
            for(var n = preamble; n < numbers.Count; n++)
            {
                var found = numbers.Skip(n - preamble).Take(preamble).Subsets(2).Select(s => s[0] + s[1]).Any(sum => sum == numbers[n]);
                if (!found) return numbers[n];
            }
            throw new InvalidOperationException("No solution found");
        }
    }
}
