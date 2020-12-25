using static MoreLinq.Extensions.ToDelimitedStringExtension;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode2020
{
    /// <summary>
    /// based on answer from here https://www.reddit.com/r/adventofcode/comments/kg1mro/2020_day_19_solutions/ggc2dcx/?utm_source=reddit&utm_medium=web2x&context=3
    /// </summary>
    public class Day19 : ISolver
    {
        public (string, string) ExpectedResult => ("156", "363");

        public (string, string) Solve(string[] input)
        {
            var part1 = Solve(input, false);
            var part2 = Solve(input, true);
            return (part1.ToString(), part2.ToString());
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

        public int Solve(IEnumerable<string> lines, bool part2)
        {
            var ans = 0;
            foreach (var line in lines)
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
                    if (Match(line, 0, line.Length, "0"))
                    {
                        ans += 1;
                    }
                }
            }
            return ans;
        }

    }
}
