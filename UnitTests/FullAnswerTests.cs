using AdventOfCode2020;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnitTests
{
    public class FullAnswerTests
    {
        public static IEnumerable<TestCaseData> FindTests()
        {
            var solvers = typeof(Day1).Assembly.GetTypes().Where(t => t.IsClass && typeof(ISolver).IsAssignableFrom(t));
            foreach(var solver in solvers)
            {
                var input = File.ReadAllLines(Utils.FindPath($"Input\\{solver.Name}.txt"));
                yield return new TestCaseData((ISolver)Activator.CreateInstance(solver), input).SetName(solver.Name);
            }
        }

        [TestCaseSource(nameof(FindTests))]
        public void RunAllDays(ISolver solver, string[] input)
        {
            var solution = solver.Solve(input);
            Assert.AreEqual(solver.ExpectedResult, solution);
        }
    }
}