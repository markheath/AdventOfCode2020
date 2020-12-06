using MoreLinq;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day6 : ISolver
    {
        public (string, string) ExpectedResult => ("6416","3050");

        public (string, string) Solve(string[] input)
        {
            var anyCount = input.Split("").Sum(batch => batch.SelectMany(b => b).Distinct().Count());
            var allCount = input.Split("").Sum(batch => batch.First().Count(c => batch.All(x => x.Contains(c))));
            return (anyCount.ToString(), allCount.ToString());
        }
    }
}
