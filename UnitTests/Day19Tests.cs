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

    }
}