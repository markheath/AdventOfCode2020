using static MoreLinq.Extensions.PairwiseExtension;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day10 : ISolver
    {
        public (string, string) ExpectedResult => ("2343", "31581162962944");

        public (string, string) Solve(string[] input)
        {
            var adapters = ParseAdapters(input);
            var part1 = UseAll(adapters);
            var part2 = CountCombinations(adapters);
            return (part1.ToString(), part2.ToString());
        }
        public static List<int> ParseAdapters(string[] input)
        {
            var adapters = input.Select(int.Parse)
                .Prepend(0)
                .OrderBy(a => a)
                .ToList();
            var builtInAdapter = adapters.Last() + 3;
            adapters.Add(builtInAdapter);
            return adapters;
        }

        public static long CountCombinations(List<int> adapters)
        {
            var index = adapters.Count - 1;
            var optionsFrom = new Dictionary<int, long>();
            optionsFrom[index] = 1;
            while (--index >= 0)
            {
                var currentAdapter = adapters[index];
                var options = 0L;
                for(int next = 1; next <= 3 && index + next < adapters.Count; next++)
                {
                    if (Accepts(adapters[index + next],currentAdapter))
                        options += optionsFrom[index+next];
                }
                optionsFrom[index] = options;
            }
            return optionsFrom[0];
        }

        public static int UseAll(IEnumerable<int> adapters)
        {
            var (jump1,jump3) = adapters
                .Pairwise((a, b) => b - a)
                .Aggregate((Jump1: 0, Jump3: 0), (acc, n) => n == 1 ? (acc.Jump1 + 1, acc.Jump3) : n == 3 ? (acc.Jump1, acc.Jump3 + 1) : acc);
            return jump1 * jump3;
        }

        public static bool Accepts(int from, int to)
        {
            var diff = from - to;
            return diff >= 1 && diff <= 3;
        }
    }
}
