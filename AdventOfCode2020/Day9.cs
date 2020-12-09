using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day9 : ISolver
    {
        public (string, string) ExpectedResult => ("1124361034", "129444555");

        public (string, string) Solve(string[] input)
        {
            var numbers = input.Select(i => long.Parse(i)).ToList();
            var part1 = Solve(numbers, 25);
            var part2 = Solve2(numbers, part1);
            return (part1.ToString(), part2.ToString());
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

        public static long Solve2(IList<long> numbers, long target)
        {
            for (var n = 0; n < numbers.Count; n++)
            {
                var total = 0L;
                for (var k = n; k < numbers.Count && total < target; k++)
                {
                    total += numbers[k];
                    if (total == target && n != k)
                    {
                        var lowest = numbers.Skip(n).Take(k - n).Min();
                        var highest = numbers.Skip(n).Take(k - n).Max();
                        return lowest + highest;
                    }
                }
            }
            throw new InvalidOperationException("No solution found");
        }
    }
}
