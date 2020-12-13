using MoreLinq;
using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day13 : ISolver
    {
        public (string, string) ExpectedResult => ("410", "");

        public (string, string) Solve(string[] input)
        {
            var earliestLeavingTime = int.Parse(input[0]);
            var busses = input[1].Split(',').Where(x => x != "x").Select(int.Parse).ToList();
            var leaving = MoreEnumerable.Generate(earliestLeavingTime, n => n + 1)
                .Select(m => new { Min = m, Bus = busses.FirstOrDefault(b => m % b == 0) })
                .First(x => x.Bus != 0);
            var part1 = (leaving.Min - earliestLeavingTime) * leaving.Bus;
            return (part1.ToString(), "");
        }
    }
}
