using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day14 : ISolver
    {
        public (string, string) ExpectedResult => ("10452688630537", "");

        public (string, string) Solve(string[] input)
        {
            var memory = new Dictionary<long, long>();
            long andMask = 0;
            long orMask = 0;
            foreach(var instruction in input)
            {
                if (instruction.StartsWith("mask = "))
                {
                    (andMask, orMask) = ParseMask(instruction.Split(' ').Last());
                }
                else
                {
                    var parts = Regex.Split(instruction, "\\D+");
                    var location = long.Parse(parts[1]);
                    var value = long.Parse(parts[2]);
                    memory[location] = (value & andMask) | orMask;
                }
            }
            var part1 = memory.Sum(m => m.Value);
            return (part1.ToString(), "");
        }

        public static (long,long) ParseMask(string mask)
        {
            long andWith = 0;
            long orWith = 0; // set all the 1s in here           
            var bit = 0;
            foreach(var c in mask.Reverse())
            {
                if (c == 'X')
                {
                    andWith |= 1L << bit;
                }
                else if (c == '1')
                {
                    orWith |= 1L << bit;
                }
                bit++;
            }
            return (andWith, orWith);
        }

    }
}
