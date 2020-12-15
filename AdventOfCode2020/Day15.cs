using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day15 : ISolver
    {
        public (string, string) ExpectedResult => ("763", "");

        public (string, string) Solve(string[] input)
        {
            var part1 = SolvePart1(input[0].Split(',')
                            .Select(int.Parse).ToList());
            return (part1.ToString(), "");
        }

        public static int SolvePart1(IList<int> starting)
        {
            var memory = new Dictionary<int, int>();             

            var last = 0;


            for (var turn = 1; turn <= 2020; turn++)
            {
                var newNumber = 0;
                if (turn <= starting.Count)
                {
                    newNumber = starting[turn - 1];
                }
                else
                {
                    if (memory.ContainsKey(last))
                    {
                        newNumber = turn - 1 - memory[last];
                    }
                }

                if (turn > 1)
                {
                    memory[last] = turn - 1;
                }
                last = newNumber;
            }
            return last;
        }
    }
}
