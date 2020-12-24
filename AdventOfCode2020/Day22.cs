using System;
using static MoreLinq.Extensions.SplitExtension;
using static MoreLinq.Extensions.ToDelimitedStringExtension;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day22 : ISolver
    {
        public (string, string) ExpectedResult => ("32815", "");
        private const int MaxCards = 51; // hack to solve thinking count is 0 when you have all the cards
        public class Hand
        {
            private int readPos = 0;
            private int writePos = 0;
            private int[] buffer = new int[MaxCards];
            public Hand(int player, IEnumerable<int> cards)
            {
                foreach (var c in cards) buffer[writePos++] = c;
                Player = player;
            }
            public int Dequeue()
            {
                var c = buffer[readPos++];
                if (readPos >= buffer.Length) readPos = 0;
                return c;
            }
            public void Enqueue(int c1, int c2)
            {
                Enqueue(c1);
                Enqueue(c2);
            }

            public void Enqueue(int c)
            {
                buffer[writePos++] = c;
                if (writePos >= buffer.Length) writePos = 0;
            }

            public override string ToString()
            {
                return All().ToDelimitedString(",");
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

            public long Score { get => All().Reverse().Select((c, index) => (long)c * (index + 1)).Sum(); }
            public int Player { get; }

            public IEnumerable<int> All()
            {
                var c = Count;
                for (var p = readPos; p < readPos + c; p++)
                {
                    yield return buffer[p % buffer.Length];
                }
            }

            public Hand Copy(int cards)
            {
                return new Hand(Player, All().Take(cards));
            }
        }

        public static Hand RecursiveCombat(Hand p1, Hand p2)
        {
            var history = new HashSet<string>();
            do
            {
                Hand roundWinner;
                // both draw their top card
                var c1 = p1.Dequeue();
                var c2 = p2.Dequeue();
                // if both players have at least as many cards in their deck as value they just drew...
                if (p1.Count >= c1 && p2.Count >= c2)
                {
                    // we're playing recursive combat
                    // (the quantity of cards copied is equal to the number on the card they drew to trigger the sub-game)
                    var recWinner = RecursiveCombat(p1.Copy(c1), p2.Copy(c2));
                    roundWinner = recWinner.Player == 1 ? p1 : p2;
                }
                else
                {
                    // the winner of the round is the player with the higher - value card.
                    if (c1 > c2) roundWinner = p1;
                    else roundWinner = p2; // no two cards the same
                }

                // enqueue winners cards - winners card always goes first
                if (roundWinner == p1) p1.Enqueue(c1, c2); else p2.Enqueue(c2, c1);

                // if history contains this state, if it exists player 1 is game winner
                var state = $"{p1}:{p2}";
                if (history.Contains(state)) return p1;
                history.Add(state);
            } while (p1.Count > 0 && p2.Count > 0);
            var gameWinner = p1.Count == 0 ? p2 : p1;
            return gameWinner;
        }


        public (string, string) Solve(string[] input)
        {
            Hand p1, p2;
            ParseHands(input, out p1, out p2);
            var round = 0;
            do
            {
                var c1 = p1.Dequeue();
                var c2 = p2.Dequeue();
                if (c1 > c2)
                {
                    p1.Enqueue(c1, c2);
                }
                else
                {
                    p2.Enqueue(c2, c1);
                }
                round++;
            } while (p1.Count > 0 && p2.Count > 0);

            var winner = p1.Count == 0 ? p2 : p1;
            var part1 = winner.Score;

            ParseHands(input, out p1, out p2);
            var part2 = RecursiveCombat(p1, p2).Score;
            return (part1.ToString(), part2.ToString());
        }

        private static void ParseHands(string[] input, out Hand p1, out Hand p2)
        {
            var hands = input.Split("").Select(c => c.Skip(1).Select(int.Parse).ToList()).ToArray();
            p1 = new Hand(1, hands[0]);
            p2 = new Hand(2, hands[1]);
        }
    }
}
