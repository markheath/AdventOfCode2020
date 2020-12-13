using MoreLinq;
using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day13 : ISolver
    {
        public (string, string) ExpectedResult => ("410", "600691418730595");

        public (string, string) Solve(string[] input)
        {
            var earliestLeavingTime = long.Parse(input[0]);
            var busses = input[1].Split(',').Where(x => x != "x").Select(int.Parse).ToList();
            var leaving = MoreEnumerable.Generate(earliestLeavingTime, n => n + 1)
                .Select(m => new { Min = m, Bus = busses.FirstOrDefault(b => m % b == 0) })
                .First(x => x.Bus != 0);
            var part1 = (leaving.Min - earliestLeavingTime) * leaving.Bus;

            var part2 = Solve2Optimized(input[1]);
            return (part1.ToString(), part2.ToString());
        }

        /// <summary>
        /// My original attempt at solving part 2 which was way too slow
        /// </summary>
        public static long Solve2(string input)
        {
            var pattern = input.Split(',').Select(x => x == "x" ? -1 : long.Parse(x)).ToList();

            for (var n = pattern[0]; ; n += pattern[0])
            {
                bool match = true;
                for (var p = 1; p < pattern.Count; p++)
                {
                    if (pattern[p] != -1)
                    {
                        if ((n + p) % pattern[p] != 0)
                        {
                            match = false;
                            break;
                        }
                    }
                }
                if (match) return n;
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// optimizing this was hard, so I ended up learning from others 
        /// solutions on Advent of Code Reddit
        /// this one inspired by Zuuou answer was simplest to understand and
        /// closest to own attempts
        /// many people referenced "Chinese Remainder Theorem"
        /// </summary>
        public static long Solve2Optimized(string input)
        {
            var pattern = input.Split(',').Select(x => x == "x" ? -1 : long.Parse(x)).ToList();
            var time = 0L;
            var inc = pattern[0];
            for (var p = 1; p < pattern.Count; p++)
            {
                if (pattern[p] != -1)
                {
                    var newTime = pattern[p];
                    while (true)
                    {
                        time += inc;
                        if ((time + p) % newTime == 0)
                        {
                            inc *= newTime;
                            break;
                        }
                    }
                }
            }
            return time;
        }
    }
}
