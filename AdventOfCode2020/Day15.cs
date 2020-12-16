using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day15 : ISolver
    {
        public (string, string) ExpectedResult => ("763", "1876406");

        public (string, string) Solve(string[] input)
        {
            var part1 = Solve(input[0].Split(',')
                            .Select(int.Parse).ToList(), 2020);
            var part2 = Solve(input[0].Split(',')
                .Select(int.Parse).ToList(), 30000000);
            return (part1.ToString(), part2.ToString());
        }

        public static int Solve(IList<int> starting, int turns)
        {
            var memory = new Dictionary<int, int>();         
            var last = 0;

            foreach (var turn in Enumerable.Range(0, turns))
            {
                var newNumber = 0;
                if (turn < starting.Count)
                {
                    newNumber = starting[turn];
                }
                else if (memory.ContainsKey(last))
                {
                    newNumber = turn - memory[last];
                }
                
                // only remember the last number after generating the new number
                if (turn > 0)
                {
                    memory[last] = turn;
                }
                last = newNumber;
            }
            return last;
        }
    }
}
