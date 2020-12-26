using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Utils
    {
        public static string FindPath(string fileName, string path = ".")
        {
            var fullPath = Path.Combine(path, fileName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            var parent = Directory.GetParent(path);
            if (parent == null) return null;
            return FindPath(fileName, parent.FullName);
        }

        public static IEnumerable<(ISolver Solver, string[] Input)> FindAllSolvers()
        {
            var solvers = typeof(Day1).Assembly.GetTypes()
                .Where(t => t.IsClass && typeof(ISolver).IsAssignableFrom(t))
                .OrderByDescending(t => int.Parse(Regex.Match(t.Name, "\\d+").Value));
            foreach (var solver in solvers)
            {
                var path = FindPath($"Input\\{solver.Name}.txt");
                if (path != null)
                {
                    var input = File.ReadAllLines(path);
                    yield return ((ISolver)Activator.CreateInstance(solver), input);
                }
            }
        }
    }
}
