using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day8 : ISolver
    {
        public (string, string) ExpectedResult => ("1384", "761");

        public (string, string) Solve(string[] input)
        {
            var program = input.Select(ParseInstruction).ToList();
            var (_,acc) = ExecuteProgram(program);

            // part 2
            Instruction lastToggled = null;
            foreach(var instruction in program)
            {
                if(instruction.Toggle())
                {
                    // put the last one back
                    if (lastToggled != null)
                    {
                        lastToggled.Toggle();
                    }
                    lastToggled = instruction;
                    var (normalExit, acc2) = ExecuteProgram(program);
                    if (normalExit)
                    {
                        return (acc.ToString(), acc2.ToString());
                    }
                }
            }
            throw new InvalidOperationException("Couldn't find completed");
        }

        private static (bool, int) ExecuteProgram(IList<Instruction> program)
        {
            var index = 0;
            var accumulator = 0;
            var visited = new HashSet<int>();
            while(index < program.Count && index >= 0)
            {
                if (visited.Contains(index))
                {
                    // return with infinite loop
                    return (false, accumulator);
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
            if (index == program.Count)
            {
                // return executed successfully
                return (true, accumulator);
            }
            else
            {
                throw new InvalidOperationException($"Went out of bounds {index}");
            }
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
            public bool Toggle()
            {
                if (Operator == "nop")
                {
                    Operator = "jmp";
                    return true;
                }
                if (Operator == "jmp")
                {
                    Operator = "nop";
                    return true;
                }
                return false;
            }
        }

    }
}
