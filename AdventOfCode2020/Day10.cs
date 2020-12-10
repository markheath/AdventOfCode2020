using MoreLinq;
using System;
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
            var part1 = UseAll(0, 0, adapters, 0);
            var part2 = CountCombinations(adapters);
            return (part1.ToString(), part2.ToString());
        }
        public static List<Adapter> ParseAdapters(string[] input)
        {
            var adapters = input.Select(int.Parse).Select(j => new Adapter(j))
                .OrderBy(a => a.Joltage)
                .ToList();
            var builtInAdapter = new Adapter(adapters.Max(a => a.Joltage) + 3);
            adapters.Add(builtInAdapter);
            return adapters;
        }

        public static long CountCombinations(List<Adapter> adapters)
        {
            adapters.Insert(0, new Adapter(0));
            var index = adapters.Count - 1;
            var optionsFrom = new Dictionary<int, long>();
            optionsFrom[index] = 1;
            while (--index >= 0)
            {
                var currentAdapter = adapters[index];
                var options = 0L;
                for(int next = 1; next <= 3 && index + next < adapters.Count; next++)
                {
                    if (adapters[index + next].CanAccept(currentAdapter.Joltage))
                        options += optionsFrom[index+next];
                }
                optionsFrom[index] = options;
            }
            return optionsFrom[0];
        }

        public static int UseAll(int jump1, int jump3, List<Adapter> remaining, int currentJoltage)
        {
            foreach(var adapter in remaining.Where(a => a.CanAccept(currentJoltage)))
            {
                var j1 = jump1;
                var j3 = jump3;
                if (adapter.Joltage - currentJoltage == 3)
                    j3++;
                if (adapter.Joltage - currentJoltage == 1)
                    j1++; 
                if (remaining.Count == 1)
                {

                    // found solution
                    return j1 * j3;
                }
                else
                {
                    var solution = UseAll(j1, j3, remaining.Where(a => a != adapter).ToList(), adapter.Joltage);
                    if (solution != -1)
                        return solution;
                }
            }
            return -1;
        }

        public class Adapter
        {
            public Adapter(int j)
            {
                Joltage = j;
            }
            public int Joltage { get; set; }
            public bool CanAccept(int inputJoltage)
            {
                var diff = Joltage - inputJoltage;
                return diff >= 1 && diff <= 3;
            }
        }
    }
}
