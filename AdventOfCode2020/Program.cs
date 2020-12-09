using System;
using System.IO;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {            
            var solver = new Day9();
            var input =  File.ReadAllLines(Utils.FindPath($"Input/{solver.GetType().Name}.txt"));
            var (a,b) = solver.Solve(input);
            Console.WriteLine($"ResultA: {a}");
            Console.WriteLine($"ResultB: {b}");
            if (solver.ExpectedResult != (a,b))
            {
                Console.WriteLine($"Error! Expected: {solver.ExpectedResult}");
            }
            else
            {
                Console.WriteLine("Success!");
            }
        }
    }
}
