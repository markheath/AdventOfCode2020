using System;
using System.IO;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var solver = new Day6();
            var input = File.ReadAllLines("../../../day6.txt");
            var (a,b) = solver.Solve(input);
            Console.WriteLine($"ResultA: {a}");
            Console.WriteLine($"ResultB: {b}");

        }

    }
}
