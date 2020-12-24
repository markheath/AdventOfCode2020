using System;
using static MoreLinq.Extensions.SplitExtension;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AdventOfCode2020
{
    public class Day22 : ISolver
    {
        public (string, string) ExpectedResult => ("32815", "");
        private const int MaxCards = 50;
        public class Hand
        {
            private int readPos = 0;
            private int writePos = 0;
            private int[] buffer = new int[MaxCards];
            public Hand(IEnumerable<int> cards)
            {
                foreach (var c in cards) buffer[writePos++] = c;
            }
            public int Dequeue()
            {
                var c = buffer[readPos++];
                if (readPos >= buffer.Length) readPos = 0;
                return c;
            }
            public void Enqueue(int c)
            {
                buffer[writePos++] = c;
                if (writePos >= buffer.Length) writePos = 0;
            }

            public int Count
            { 
                get 
                { 
                    var count = writePos - readPos;
                    if (count < 0) count += buffer.Length;
                    return count;
                }
            }
            public IEnumerable<int> All()
            {
                while(Count > 0)
                {
                    yield return Dequeue();
                }
            }            
        }



        public (string, string) Solve(string[] input)
        {
            var hands = input.Split("").Select(c => c.Skip(1).Select(int.Parse).ToList()).ToArray();
            var p1 = new Hand(hands[0]);
            var p2 = new Hand(hands[1]);
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
            var part1 = winner.All().Reverse().Select((c, index) => (long)c * (index + 1)).Sum();
            return (part1.ToString(), "");
        }
    }
}
