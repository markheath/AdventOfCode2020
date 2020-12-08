using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day8Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".Split("\r\n");
            var solver = new Day8();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("5", ""), solution);
        }

        [Test]
        public void CanParseInstruction()
        {
            var ins = Day8.ParseInstruction("acc +6");
            Assert.AreEqual("acc", ins.Operator);
            Assert.AreEqual(6, ins.Argument);
        }
    }

}