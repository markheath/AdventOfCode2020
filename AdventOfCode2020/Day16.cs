using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day16 : ISolver
    {
        public (string, string) ExpectedResult => ("23044", "");

        public (string, string) Solve(string[] input)
        {
            var parts = input.Split("").ToList();
            var rules = parts[0].Select(r => new Rule(r)).ToList();
            var myTicket = new Ticket(parts[1].Last());
            var nearbyTickets = parts[2].Skip(1).Select(t => new Ticket(t)).ToList();
            var scanningError = nearbyTickets.Sum(n => n.ScanningError(rules));

            return (scanningError.ToString(), "");
        }

        public class Ticket
        {
            private readonly string ticket;
            public override string ToString() => ticket;
            private int[] values;
            public Ticket(string ticket)
            {
                values = ticket.Split(',').Select(int.Parse).ToArray();
                this.ticket = ticket;
            }
            public int ScanningError(IEnumerable<Rule> rules)
            {
                return values.Where(v => rules.All(r => !r.IsValid(v))).Sum();
            }
        }

        public class Rule
        {
            private readonly string rule;
            public override string ToString() => rule;
            private (int, int) range1;
            private (int, int) range2;
            public Rule(string rule)
            {
                var idx = rule.IndexOf(':');
                Name = rule.Substring(0, idx);
                var parts = rule.Substring(idx+2).Split(" or ");
                range1 = (int.Parse(parts[0].Split('-')[0]), int.Parse(parts[0].Split('-')[1]));
                range2 = (int.Parse(parts[1].Split('-')[0]), int.Parse(parts[1].Split('-')[1]));
                this.rule = rule;
            }
            public string Name { get; }
            public bool IsValid(int n)
            {
                return (n >= range1.Item1 && n <= range1.Item2) ||
                    (n >= range2.Item1 && n <= range2.Item2);
            }

        }

    }
}
