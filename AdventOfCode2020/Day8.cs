using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day8 : ISolver
    {
        public (string, string) ExpectedResult => ("1384", "");

        public (string, string) Solve(string[] input)
        {
            var program = input.Select(ParseInstruction).ToList();
            var acc = ExecuteProgram(program);
            return (acc.ToString(), "");
        }

        private static int ExecuteProgram(IList<Instruction> program)
        {
            var index = 0;
            var accumulator = 0;
            var visited = new HashSet<int>();
            while(index < program.Count &&index >= 0)
            {
                if (visited.Contains(index))
                {
                    // infinite loop detected, return;
                    break;
                }
                visited.Add(index);
                var currentInstruction = program[index];
                switch(currentInstruction.Operator)
                {
                    case "acc":
                        accumulator += currentInstruction.Argument;
                        index++;
                        break;
                    case "jmp":
                        index += currentInstruction.Argument;
                        break;
                    case "nop":
                        index++;
                        break;
                    default:
                        throw new NotImplementedException("Unknown instruction " + currentInstruction.Operator);
                }
            }
            return accumulator;
        }

        public static Instruction ParseInstruction(string instruction)
        {
            var parts = instruction.Split(' ');
            return new Instruction() { Operator = parts[0], Argument = int.Parse(parts[1]) };
        }

        public class Instruction
        {
            public string Operator { get; set; }
            public int Argument { get; set; }
        }

    }
}
