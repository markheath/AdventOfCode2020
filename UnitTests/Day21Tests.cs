using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day21Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)".Split("\r\n");
            var answer = new Day21().Solve(testInput);
            Assert.AreEqual(("5", ""), answer);
        }
    }
}