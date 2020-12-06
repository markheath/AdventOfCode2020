using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day6 : ISolver
    {
        public (string, string) ExpectedResult => ("6416","3050");

        public (string, string) Solve(string[] input)
        {

            var anyCount = 0;
            var allCount = 0;
            foreach (var batch in input.Split(""))
            {
                var anyAnswer = new HashSet<char>();
                foreach (var line in batch)
                {
                    foreach (var c in line)
                    {
                        anyAnswer.Add(c);
                    }
                }
                foreach(var c in batch.First())
                {
                    if(batch.All(x => x.Contains(c)))
                    {
                        allCount++;
                    }
                }

                anyCount += anyAnswer.Count;
            }
            return (anyCount.ToString(), allCount.ToString());
        }
    }
}
