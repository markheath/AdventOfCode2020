using System;
using System.Collections.Generic;
using System.Linq;
using static MoreLinq.Extensions.SplitExtension;

namespace AdventOfCode2020
{
    public class Day16 : ISolver
    {
        public (string, string) ExpectedResult => ("23044", "3765150732757");

        public (string, string) Solve(string[] input)
        {
            var (myTicket, rules, nearbyTickets) = ParseInput(input);
            int part1 = TotalScanningError(rules, nearbyTickets);

            var positionRules = SolvePositionRules(rules, nearbyTickets);

            // part 4 -multiply six fields on your ticket that start with the word departure
            var part2 = positionRules
                            .Where(kvp => kvp.Value.Name.StartsWith("departure"))
                            .Select(kvp => (long)myTicket.Values[kvp.Key])
                            .Aggregate((a, b) => a * b);

            return (part1.ToString(), part2.ToString());
        }

        public static Dictionary<int, Rule> SolvePositionRules(List<Rule> rules, List<Ticket> nearbyTickets)
        {
            // part 2 step 1 - find valid tickets (checking scanning error doesn't work - probably some invalid tickets have 0 values)
            var validTickets = nearbyTickets.Where(t => t.IsValid(rules)).ToList();

            // part 2 step 2 - eliminate invalid rules for each position
            var possibleRules = new Dictionary<int, HashSet<Rule>>();
            var positions = nearbyTickets.First().Values.Length;
            for (var position = 0; position < positions; position++)
            {
                possibleRules[position] = rules.ToHashSet();
                foreach (var t in validTickets)
                {
                    foreach (var r in rules)
                    {
                        if (!r.IsValid(t.Values[position]))
                        {
                            possibleRules[position].Remove(r);
                        }
                    }
                }
            }

            // part 3 - pick out the one rule for each position
            var positionRules = new Dictionary<int, Rule>();
            while (possibleRules.Count > 0)
            {
                var solved = possibleRules.First(kvp => kvp.Value.Count == 1);
                var solvedRule = solved.Value.Single();
                positionRules.Add(solved.Key, solvedRule);
                possibleRules.Remove(solved.Key);
                // exclude this rule from all unsolved position
                foreach (var (_,pr) in possibleRules)
                {
                    pr.Remove(solvedRule);
                }
            }

            return positionRules;
        }

        public static (Ticket, List<Rule>, List<Ticket>) ParseInput(string[] input)
        {
            var parts = input.Split("").ToList();
            var myTicket = new Ticket(parts[1].Last());
            var rules = parts[0].Select(r => new Rule(r)).ToList();
            var nearbyTickets = parts[2].Skip(1).Select(t => new Ticket(t)).ToList();
            return (myTicket, rules, nearbyTickets);
        }

        // part 1
        public static int TotalScanningError(IEnumerable<Rule> rules, IEnumerable<Ticket> nearbyTickets) => nearbyTickets.Sum(n => n.ScanningError(rules));

        public class Ticket
        {
            private readonly string ticket;
            public override string ToString() => ticket;
            public int[] Values { get;  }
            public Ticket(string ticket)
            {
                Values = ticket.Split(',').Select(int.Parse).ToArray();
                this.ticket = ticket;
            }
            public int ScanningError(IEnumerable<Rule> rules)
            {
                return Values.Where(v => rules.All(r => !r.IsValid(v))).Sum();
            }
            public bool IsValid(IEnumerable<Rule> rules)
            {
                // it's invalid if there is any value which doesn't meet any rules
                var invalid = Values.Any(v => rules.All(r => !r.IsValid(v)));
                return !invalid;
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
                Name = rule[0..idx];
                var parts = rule[(idx + 2)..].Split(" or ");
                range1 = (int.Parse(parts[0].Split('-')[0]), int.Parse(parts[0].Split('-')[1]));
                range2 = (int.Parse(parts[1].Split('-')[0]), int.Parse(parts[1].Split('-')[1]));
                this.rule = rule;
            }
            public string Name { get; }
            public bool IsValid(int value)
            {
                return (value >= range1.Item1 && value <= range1.Item2) ||
                    (value >= range2.Item1 && value <= range2.Item2);
            }

        }

    }
}
