using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day23 : ISolver
    {
        public (string, string) ExpectedResult => ("98742365", "294320513093");

        private static int[] lookup = null;

        public static int Parse(string input, int length =0)
        {
            if (length == 0) length = input.Length;
            return Parse(input.Select(c => c - '0').Concat(Enumerable.Range(input.Length + 1, length - input.Length)), length);
        }

        public static int Parse(IEnumerable<int> ids, int length)
        {
            int firstCup = -1;
            int currentCup = -1;
            lookup = new int[length+1];
            foreach (var cup in ids)
            {
                if (firstCup == -1) firstCup = cup;
                if (currentCup != -1) lookup[currentCup] = cup;
                currentCup = cup;
            }
            // complete the circle
            lookup[currentCup] = firstCup;
            return firstCup;
        }
        public static int Move(int cup, int moves)
        {
            var maxId = lookup.Length - 1;
            for (var n = 0; n < moves; n++)
            {
                cup = MoveOne(cup, maxId);
            }
            return cup;
        }

        public static int MoveOne(int cup, int maxId)
        {
            // remove the three next cups
            var rem1 = lookup[cup];
            var rem2 = lookup[rem1];
            var rem3 = lookup[rem2];
            var keep = lookup[rem3];

            lookup[cup] = keep;

            // work out the destination cup
            var destCupId = cup == 1 ? maxId : cup - 1;
            while (destCupId == rem1 || destCupId == rem2 || destCupId == rem3)
            {
                destCupId--;
                if (destCupId == 0) destCupId = maxId;
            }
            
            // insert the picked up cups
            lookup[rem3] = lookup[destCupId];
            lookup[destCupId] = rem1;

            return keep;
        }


        public static string Describe(int cup)
        {
            var sb = new StringBuilder();
            var c = cup;
            do
            {
                sb.Append(c);
                c = lookup[c];
            } while (c != cup);
            return sb.ToString();
        }


        public static string Part1(string input)
        {
            var c = Parse(input, input.Length);
            c = Move(c, 100);
            return Describe(1)[1..];
        }

        public static string Part2(string input)
        {
            var c = Parse(input, 1_000_000);
            c = Move(c, 10_000_000);
            var answer  = (long)lookup[1] * lookup[lookup[1]];
            return answer.ToString();
        }

        public (string, string) Solve(string[] input)
        {
            var p1 = Part1(input[0]);
            var p2 = Part2(input[0]);
            return (p1, p2);
        }
    }
}
