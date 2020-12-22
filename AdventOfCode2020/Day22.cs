using System;
using static MoreLinq.Extensions.SplitExtension;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day22 : ISolver
    {
        public (string, string) ExpectedResult => ("32815", "");

        public (string, string) Solve(string[] input)
        {
            var hands = input.Split("").Select(c => c.Skip(1).Select(int.Parse).ToList()).ToArray();
            var p1 = new Queue<int>(hands[0]);
            var p2 = new Queue<int>(hands[1]);
            var round = 0;
            do
            {
                var c1 = p1.Dequeue();
                var c2 = p2.Dequeue();
                if (c1 > c2)
                {
                    p1.Enqueue(c1);
                    p1.Enqueue(c2);
                }
                else
                {
                    p2.Enqueue(c2);
                    p2.Enqueue(c1);
                }
                round++;
            } while (p1.Count > 0 && p2.Count > 0);

            var winner = p1.Count == 0 ? p2 : p1;
            var part1 = winner.Reverse().Select((c, index) => (long)c * (index + 1)).Sum();
            return (part1.ToString(), "");
        }
    }
}
