using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day7 : ISolver
    {
        public (string, string) ExpectedResult => ("246", "2976");

        public (string, string) Solve(string[] input)
        {
            var lookup = input.Select(r => ParseBagRule(r)).ToDictionary(a => a.Color, a => a);
            foreach(var bag in lookup.Values)
            {
                foreach(var content in bag.Contents)
                {
                    content.Bag = lookup[content.Color];
                }
            }

            var total = 0;
            foreach(var b in lookup.Values.Where(b => b.Color != "shiny gold"))
            {
                if (FullContents(b).Any(b => b.Color == "shiny gold")) total++;
            }
            var count = Count(lookup["shiny gold"]);
            return (total.ToString(), count.ToString());
        }

        private static IEnumerable<Bag> FullContents(Bag bag)
        {
            yield return bag;
            foreach(var b in bag.Contents)
            {
                foreach (var c in FullContents(b.Bag))
                    yield return c;
            }
        }

        private static int Count(Bag bag)
        {
            var total = 0;
            foreach (var b in bag.Contents)
            {
                total += b.Count;
                total += b.Count * Count(b.Bag);
            }
            return total;
        }

        public class Bag
        {
            public Bag()
            {
                Contents = new List<BagContents>();
            }
            public string Color { get; set; }
            public List<BagContents> Contents { get; }
        }

        public class BagContents
        {
            public string Color { get; set; }
            public int Count { get; set; }
            public Bag Bag { get; set; }

        }

        public static Bag ParseBagRule(string rule)
        {
            var parts = Regex.Split(rule, @"( bags contain )|( bags?, )|( bags?\.)");


            var bag = new Bag();
            bag.Color = parts[0];
            for(int n = 2; n < parts.Length - 1; n+=2)
            {
                var part = parts[n];
                if (part == "no other") break;
                var count = int.Parse(part.Substring(0, 1));
                var col = part.Substring(2).Trim();
                bag.Contents.Add(new BagContents() { Color = col, Count = count });
            }
            return bag;
        }
    }
}
