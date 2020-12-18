using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day18 : ISolver
    {
        public (string, string) ExpectedResult => ("67800526776934", "");

        public (string, string) Solve(string[] input)
        {
            var part1 = input.Sum(SolveEquation);
            return (part1.ToString(), "");
        }

        public static long SolveEquation(string equation)
        {
            long currentTotal = 0;
            char currentOperand = default;
            void ApplyAction(long value)
            {
                if (currentOperand == default(char))
                    currentTotal = value;
                else if (currentOperand == '+')
                    currentTotal += value;
                else
                    currentTotal *= value;
            }
            for (var n = 0; n < equation.Length; n++)
            {
                var c = equation[n];
                if (char.IsDigit(c))
                {
                    ApplyAction(c - '0');
                }
                else if (c == '(')
                {
                    // find matching end bracket
                    var count = 1;
                    for (var k = n+1; n < equation.Length; k++)
                    {
                        if (equation[k] == ')')
                        {
                            count--;
                            if (count == 0)
                            {
                                var subEquation = equation.Substring(n + 1, k - n - 1);
                                ApplyAction(SolveEquation(subEquation));
                                n = k + 1; // jump past this equation
                                break;
                            }
                        }
                        else if (equation[k] == '(')
                        {
                            count++;
                        }
                    }
                    if (count != 0) throw new InvalidOperationException("failed to parse subequation");
                }
                else if (c == '+' || c == '*')
                {
                    currentOperand = c;
                }

            }
            return currentTotal;
        }
    }
}
