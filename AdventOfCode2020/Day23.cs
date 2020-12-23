using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day23 : ISolver
    {
        public (string, string) ExpectedResult => ("98742365", "");

        public class Cup
        {
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
                foreach (var id in ids)
                {
                    var cup = new Cup() { Id = id, Previous = currentCup };
                    if (firstCup == null) firstCup = cup;
                    if (currentCup != null) currentCup.Next = cup;
                    currentCup = cup;
                }
                // complete the circle
                currentCup.Next = firstCup;
                firstCup.Previous = currentCup;
                return firstCup;
            }

            public int Id { get; set; }
            public Cup Previous { get; set; }
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
                var sw = Stopwatch.StartNew();
                for (var n = 0; n < moves; n++)
                {
                    cup = cup.MoveOne(maxId);
                    if (n % 50_000 == 0) Console.WriteLine($"{sw.Elapsed} {n}");
                }
                return cup;
            }

            public Cup MoveOne(int maxId)
            {
                // remove the three next cups
                var firstToRemove = this.Next;
                var lastToRemove = firstToRemove.Next.Next;
                var firstToKeep = lastToRemove.Next;
                var removeIds = new HashSet<int>() { firstToRemove.Id, firstToRemove.Next.Id, lastToRemove.Id };
                this.Next = firstToKeep;
                firstToKeep.Previous = this;

                // work out the destination cup
                var destCupId = Id == 1 ? maxId : Id - 1;
                while (removeIds.Contains(destCupId))
                {
                    destCupId--;
                    if (destCupId == 0) destCupId = maxId;
                }
                var destCup = this.Next.Find(destCupId);
                
                // insert the picked up cups
                destCup.Next.Previous = lastToRemove;
                lastToRemove.Next = destCup.Next;
                destCup.Next = firstToRemove;
                firstToRemove.Previous = destCup;

                return Next;
            }

            public Cup Find(int id)
            {
                var testCup = this;
                do
                {
                    if (testCup.Id == id)
                    {
                        return testCup;
                    }
                    testCup = testCup.Next;
                } while (testCup != this);
                return null;
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
            return (p1, "");
        }
    }
}
