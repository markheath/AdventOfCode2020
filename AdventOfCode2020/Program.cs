using System;
using System.IO;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var day5 = new Day5();
            var input = File.ReadAllLines("../../../day5.txt");
            var (a,b) = day5.Solve(input);
            Console.WriteLine($"ResultA: {a}");
            Console.WriteLine($"ResultB: {b}");

        }

    }
}
