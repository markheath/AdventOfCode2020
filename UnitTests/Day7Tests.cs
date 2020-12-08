using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day7Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.".Split("\r\n");
            var solver = new Day7();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("4", "32"), solution);
        }

        [Test]
        public void CountBags()
        {
            var testInput = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags".Split("\r\n");
        var solver = new Day7();
        var (_,count) = solver.Solve(testInput);
        Assert.AreEqual("126", count);
            }

        [Test]
        public void ParseBagRule()
        {
            var bag = Day7.ParseBagRule("light red bags contain 1 bright white bag, 2 muted yellow bags.");
            Assert.AreEqual("light red", bag.Color);
            Assert.AreEqual(2, bag.Contents.Count);
            Assert.AreEqual("bright white", bag.Contents[0].Color);
            Assert.AreEqual("muted yellow", bag.Contents[1].Color);
            Assert.AreEqual(1, bag.Contents[0].Count);
            Assert.AreEqual(2, bag.Contents[1].Count);
        }

        [Test]
        public void ParseEmptyBagRule()
        {
            var bag = Day7.ParseBagRule("light red bags contain no other bags.");
            Assert.AreEqual("light red", bag.Color);
            Assert.AreEqual(0, bag.Contents.Count);
        }
    }

}