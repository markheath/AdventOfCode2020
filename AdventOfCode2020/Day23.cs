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

        public class Cup
        {
            private static Dictionary<int, Cup> lookup = new Dictionary<int, Cup>();

            public static Cup Parse(string input)
            {
                return Parse(input.Select(c => c - '0'));
            }

            public static Cup Parse2(string input)
            {
                return Parse(input.Select(c => c - '0').Concat(Enumerable.Range(input.Length+1, 1_000_000-input.Length)));
            }

            public static Cup Parse(IEnumerable<int> ids)
            {
                Cup firstCup = null;
                Cup currentCup = null;
                lookup.Clear();
                foreach (var id in ids)
                {
                    var cup = new Cup() { Id = id };
                    if (firstCup == null) firstCup = cup;
                    if (currentCup != null) currentCup.Next = cup;
                    currentCup = cup;
                    lookup[id] = cup;
                }
                // complete the circle
                currentCup.Next = firstCup;
                return firstCup;
            }

            public int Id { get; set; }
            public Cup Next { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var testCup = this;
                do
                {
                    sb.Append(testCup.Id);
                    testCup = testCup.Next;
                } while (testCup != this);
                return sb.ToString();
            }
            public Cup Move(int moves, int maxId)
            {
                var cup = this;
                for (var n = 0; n < moves; n++)
                {
                    cup = cup.MoveOne(maxId);
                }
                return cup;
            }

            public Cup Find(int cupId)
            {
                return lookup[cupId];
            }

            public Cup MoveOne(int maxId)
            {
                // remove the three next cups
                var firstToRemove = this.Next;
                var lastToRemove = firstToRemove.Next.Next;
                var firstToKeep = lastToRemove.Next;
                var id1 = firstToRemove.Id;
                var id2 = firstToRemove.Next.Id;
                var id3 = lastToRemove.Id;

                this.Next = firstToKeep;
                
                // work out the destination cup
                var destCupId = Id == 1 ? maxId : Id - 1;
                while (destCupId == id1 || destCupId == id2 || destCupId == id3)
                {
                    destCupId--;
                    if (destCupId == 0) destCupId = maxId;
                }
                var destCup = lookup[destCupId];
                
                // insert the picked up cups
                lastToRemove.Next = destCup.Next;
                destCup.Next = firstToRemove;
                
                return Next;
            }


        } 

        

        public static string Part1(string input)
        {
            var c = Cup.Parse(input);
            c = c.Move(100, 9);
            return c.Find(1).ToString()[1..];
        }

        public static string Part2(string input)
        {
            var c = Cup.Parse2(input);
            c = c.Move(10_000_000, 1_000_000);
            var cup1 = c.Find(1);
            var answer  = (long)cup1.Next.Id * (long)cup1.Next.Next.Id;
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
