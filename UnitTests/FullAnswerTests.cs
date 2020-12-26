using AdventOfCode2020;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class FullAnswerTests
    {
        public static IEnumerable<TestCaseData> FindTests()
        {
            return Utils.FindAllSolvers()
                .Select(s => new TestCaseData(s.Solver, s.Input).SetName(s.Solver.GetType().Name));
        }

        [TestCaseSource(nameof(FindTests))]
        public void RunAllDays(ISolver solver, string[] input)
        {
            var solution = solver.Solve(input);
            Assert.AreEqual(solver.ExpectedResult, solution);
        }
    }
}