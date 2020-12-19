using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day19 : ISolver
    {
        public (string, string) ExpectedResult => ("156", "");

        public (string, string) Solve(string[] input)
        {
            var part1 = CountValidMessages(input);
            return (part1.ToString(), "");
        }

        public static int CountValidMessages(string[] input)
        {
            var sections = input.Split("").ToArray();
            var ruleset = new RuleSet(sections[0]);
            return sections[1].Count(m => ruleset.IsFullMatch(m));
        }

        public class RuleSet
        {
            public Dictionary<int, Rule> Rules { get; }

            public RuleSet(IEnumerable<string> rules)
            {
                Rules = rules.Select(r => new Rule(r)).ToDictionary(r => r.Id, r => r);
            }

            public bool IsFullMatch(string message)
            {
                var (match, pos) = Check(message);
                return match && pos == message.Length;
            }

            public (bool,int) Check(string message, int startingRule = 0, int startPos = 0)
            {
                var rule = Rules[startingRule];
                if (rule.Match != null)
                {
                    if (message.Substring(startPos).StartsWith(rule.Match))
                        return (true, startPos + rule.Match.Length);
                    return (false,startPos);
                }
                else
                {
                    bool isMatched = false;
                    foreach(var sequence in rule.Sequences)
                    {
                        var sequenceMatch = false;
                        var currentPos = startPos;
                        foreach(var ruleId in sequence)
                        {
                            (isMatched, currentPos) = Check(message, ruleId, currentPos);
                            if (!isMatched) break;
                        }
                        isMatched |= sequenceMatch;
                        if (isMatched) return (true, currentPos);
                    }
                    return (false, startPos);
                }
            }

        }

        public class Rule
        {
            public int Id { get; }
            public List<List<int>> Sequences { get; }
            public string Match { get; }

            public Rule(string rule)
            {
                var bits = rule.Split(' ');
                Id = int.Parse(bits[0].Substring(0, bits[0].Length - 1));
                Sequences = new List<List<int>>();
                List<int> currentSequence = null;
                for (var n = 1; n < bits.Length; n++)
                {
                    if (bits[n].StartsWith('"'))
                    {
                        Match = bits[n].Substring(1, 1); // always 1 char
                    }
                    else if (bits[n] == "|")
                    {
                        currentSequence = null;
                    }
                    else 
                    {
                        if (currentSequence == null)
                        {
                            currentSequence = new List<int>();
                            Sequences.Add(currentSequence);
                        }
                        currentSequence.Add(int.Parse(bits[n]));
                    }
                }
            }
        }

    }
}
