using System;
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
                Cup firstCup = null;
                Cup currentCup = null;
                foreach (var ch in input)
                {
                    var cup = new Cup() { Id = ch - '0', Previous = currentCup };
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
            public Cup Move(int moves)
            {
                var cup = this;
                for (var n = 0; n < moves; n++)
                    cup = cup.Move();
                return cup;
            }

            public Cup Move()
            {
                // remove the three next cups
                var firstToRemove = this.Next;
                var lastToRemove = firstToRemove.Next.Next;
                var firstToKeep = lastToRemove.Next;
                
                this.Next = firstToKeep;
                firstToKeep.Previous = this;

                // work out the destination cup
                Cup destCup = null;
                var destCupId = this.Id;
                while (destCup == null)
                {
                    destCupId--;
                    if (destCupId == 0) destCupId = 9;

                    var testCup = this.Next;
                    while (testCup != this)
                    {
                        if (testCup.Id == destCupId)
                        {
                            destCup = testCup;
                            break;
                        }
                        testCup = testCup.Next;
                    }
                }

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
            c = c.Move(100);
            return c.Find(1).ToString()[1..];
        }

        public (string, string) Solve(string[] input)
        {
            var p1 = Part1(input[0]);
            return (p1, "");
        }
    }
}
