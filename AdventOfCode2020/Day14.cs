using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day14 : ISolver
    {
        public (string, string) ExpectedResult => ("10452688630537", "2881082759597");

        public (string, string) Solve(string[] input)
        {
            long part1 = SolvePart1(input);
            return (part1.ToString(), SolvePart2(input).ToString());
        }

        public static long SolvePart1(string[] input)
        {
            var memory = new Dictionary<long, long>();
            long andMask = 0;
            long orMask = 0;
            foreach (var instruction in input)
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
            return memory.Sum(m => m.Value);
        }

        public static long SolvePart2(string[] input)
        {
            var memory = new Dictionary<long, long>();
            string mask = "";
            foreach (var instruction in input)
            {
                if (instruction.StartsWith("mask = "))
                {
                    mask = instruction.Split(' ').Last();
                }
                else
                {
                    var parts = Regex.Split(instruction, "\\D+");
                    var location = long.Parse(parts[1]);
                    var value = long.Parse(parts[2]);
                    foreach (var loc in GetLocations(mask, location))
                        memory[loc] = value;
                }
            }
            return memory.Sum(m => m.Value);
        }


        public static IEnumerable<long> GetLocations(string mask, long location)
        {
            var variations = 1 << mask.Count(c => c == 'X');
            for(int variation = 0; variation < variations; variation++)
            {
                var andWith = 0L;
                var orWith = 0L;
                var variationBit = 0;
                var bit = 0;
                foreach (var c in mask.Reverse())
                {
                    if (c == 'X')
                    {
                        if ((variation >> variationBit) % 2 == 1)
                            orWith |= 1L << bit;
                        variationBit++;
                    }
                    else if (c == '1')
                    {
                        orWith |= 1L << bit;
                    }
                    else
                    {
                        andWith |= 1L << bit;

                    }
                    bit++;
                }
                yield return (andWith & location) | orWith;

            }
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
