using static MoreLinq.Extensions.SplitExtension;
using static MoreLinq.Extensions.ToDelimitedStringExtension;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode2020
{
    /// <summary>
    /// based on answer from here https://www.reddit.com/r/adventofcode/comments/kg1mro/2020_day_19_solutions/ggc2dcx/?utm_source=reddit&utm_medium=web2x&context=3
    /// </summary>
    public class Day19V2
    {
        private string[] lines;

        public Day19V2(string[] input)
        {
            lines = input;
        }
        private Dictionary<string, List<List<string>>> rangeRules = new Dictionary<string, List<List<string>>>();
        private Dictionary<string, string> constantRules = new Dictionary<string, string>();

        public bool MatchList(string line, int start, int end, IEnumerable<string> rules)
        {
            var r1 = rules.FirstOrDefault();
            if (start == end && r1 == null)
                return true;
            if (start == end)
                return false;
            if (r1 == null)
                return false;

            for (var i = start + 1; i < end + 1; i++)
            {
                if (Match(line, start, i, r1) && MatchList(line, i, end, rules.Skip(1)))
                {
                    return true; // another missed optimization
                }
            }
            return false;
        }

        // memoize this function
        private Dictionary<(int, int, string), bool> history = new Dictionary<(int, int, string), bool>();
        private bool Match(string line, int st, int ed, string rule)
        {
            var key = (st, ed, rule);
            if (history.ContainsKey(key))
            {
                return history[key];
            }

            var ret = false;
            if (constantRules.ContainsKey(rule))
            {
                ret = line[st..ed] == constantRules[rule]; 
            }
            else
            {
                foreach (var options in rangeRules[rule])
                {
                    if (MatchList(line, st, ed, options))
                    {
                        ret = true;
                        break; // missing optimization to break? - didn't save a lot of time
                    }
                }
            }
            history[key] = ret;
            return ret;
        }

        public int Solve(bool part2)
        {
            var ans = 0;
            foreach(var line in lines)
            {
                if (line.Contains(':'))
                {
                    var words = line.Split();
                    var name = words[0][..^1];
                    string rest;
                    if (name == "8" && part2)
                        rest = "42 | 42 8";
                    else if (name == "11" && part2)
                        rest = "42 31 | 42 11 31";
                    else
                        rest = words.Skip(1).ToDelimitedString(" ");
                    if (rest.Contains('"'))
                        constantRules[name] = rest[1..^1];
                    else
                    {
                        var options = rest.Split(" | ");
                        rangeRules[name] = options.Select(x => x.Split().ToList()).ToList();
                    }
                }
                else if (!String.IsNullOrEmpty(line))
                {
                    history.Clear();
                    if(Match(line, 0, line.Length, "0"))
                    {
                        ans += 1;
                    }
                }           
            }
            return ans;
        }
    }



    public class Day19 : ISolver
    {
        public (string, string) ExpectedResult => ("156", "363");

        public (string, string) Solve(string[] input)
        {
            var part1 = CountValidMessages(input);
            //var part2 = CountValidMessages2(input);
            var v2 = new Day19V2(input);
            var part2 = v2.Solve(true);
            return (part1.ToString(), part2.ToString());
        }

        public static int CountValidMessages(string[] input)
        {
            var sections = input.Split("").ToArray();
            var ruleset = new RuleSet(sections[0]);
            return sections[1].Count(m => ruleset.IsFullMatch(m));
        }

        public static int CountValidMessages2(string[] input)
        {
            var sections = input.Split("").ToArray();
            var ruleset = new RuleSet(sections[0]);
            ruleset.UpdateRule("8: 42 | 42 8");
            ruleset.UpdateRule("11: 42 31 | 42 11 31");
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
                    if (message[startPos..].StartsWith(rule.Match))
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
                        // bug is here need to also explore further matches as there could be more than one
                    }
                    return (false, startPos);
                }
            }

            public void UpdateRule(string ruleDef)
            {
                var rule = new Rule(ruleDef);
                Rules[rule.Id] = rule;            
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
                Id = int.Parse(bits[0][0..^1]);
                Sequences = new List<List<int>>();
                List<int> currentSequence = null;
                for (var n = 1; n < bits.Length; n++)
                {
                    if (bits[n].StartsWith('"'))
                    {
                        Match = bits[n][1..2]; // always 1 char
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
