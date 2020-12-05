using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    interface ISolver
    {
        public (string,string) Solve(string[] input);
        public (string, string) ExpectedResult { get; }
    }

    class Day5 : ISolver
    {
        public (string, string) Solve(string[] input)
        {
            var seats = ParseSeats(input);
            var partA = seats.Max().ToString(); // 965
            var partB = Enumerable.Range(0, 128 * 8).Single(s => !seats.Contains(s) && seats.Contains(s - 1) && seats.Contains(s + 1)).ToString(); // 524
            return (partA, partB);
        }

        public (string, string) ExpectedResult => ("965", "524");

        private HashSet<int> ParseSeats(string[] input) => 
            input.Select(x => Convert.ToInt32(x.Replace('F', '0').Replace('B', '1').Replace('R', '1').Replace('L', '0'), 2)).ToHashSet();
    }
}
