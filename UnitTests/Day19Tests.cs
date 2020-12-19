using AdventOfCode2020;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day19Tests
    {
        [Test]
        public void ParseRules()
        {
            var testInput = @"0: 1 2
1: ""a""
2: 1 3 | 3 1
3: ""b""".Split("\r\n");
            var rules = testInput.Select(r => new Day19.Rule(r)).ToList();
            Assert.AreEqual(0, rules[0].Id);
            Assert.AreEqual(new[] { 1, 2 }, rules[0].Sequences[0]);
            Assert.AreEqual(1, rules[0].Sequences.Count);
            Assert.IsNull(rules[0].Match);

            Assert.AreEqual(1, rules[1].Id);
            Assert.AreEqual(0, rules[1].Sequences.Count);
            Assert.AreEqual("a", rules[1].Match);

            Assert.AreEqual(2, rules[2].Id);
            Assert.AreEqual(new[] { 1, 3 }, rules[2].Sequences[0]);
            Assert.AreEqual(new[] { 3, 1 }, rules[2].Sequences[1]);
            Assert.AreEqual(2, rules[2].Sequences.Count);
            Assert.IsNull(rules[2].Match);

            Assert.AreEqual(3, rules[3].Id);
            Assert.AreEqual(0, rules[3].Sequences.Count);
            Assert.AreEqual("b", rules[3].Match);

        }

        [TestCase("ababbb",true)]
        [TestCase("abbbab", true)]
        [TestCase("bababa", false)]
        [TestCase("aaabbb", false)]
        [TestCase("aaaabbb", false)] // might be true, but not a full match
        public void ApplyRules(string message, bool valid)
        {
            var testInput = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""".Split("\r\n");
            var ruleset = new Day19.RuleSet(testInput);
            Assert.AreEqual(valid, ruleset.IsFullMatch(message));
        }

        [Test]
        public void CountValidMessages()
        {
            var testInput = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""

ababbb
bababa
abbbab
aaabbb
aaaabbb".Split("\r\n");
            Assert.AreEqual(2, Day19.CountValidMessages(testInput));
        }

        [Test]
        public void Part2TestInput()
        {
            var testInput = @"42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: ""a""
11: 42 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: ""b""
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1

abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba".Split("\r\n");
            var answer = new Day19().Solve(testInput);
            Assert.AreEqual("3", answer.Item1, "part 1");
            Assert.AreEqual("12", answer.Item2, "part 2");
        }

    }
}