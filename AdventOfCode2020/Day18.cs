using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day18 : ISolver
    {
        public (string, string) ExpectedResult => ("67800526776934", "340789638435483");

        public (string, string) Solve(string[] input)
        {
            var part1 = input.Sum(SolveEquation);
            var part2 = input.Sum(SolveEquation2);

            return (part1.ToString(), part2.ToString());
        }

        abstract class Token { }
        abstract class Operator : Token { }
        class Multiply : Token { }
        class Add : Token { }
        class Number : Token
        {
            public Number(long value)
            {
                Value = value;
            }
            public long Value { get; }
        }

        class Equation : Token
        {
            public Equation()
            {
                Tokens = new List<Token>();
            }
            public List<Token> Tokens { get; }

            public long Solve()
            {
                // 1. solve all subequations
                for (int n = 0; n < Tokens.Count; n++)
                {
                    if (Tokens[n] is Equation eq)
                    {
                        Tokens[n] = new Number(eq.Solve());
                    }
                }

                // perform all additions
                var k = 0;
                while(k < Tokens.Count)
                {
                    if (Tokens[k] is Add)
                    {
                        var newValue = ((Number)Tokens[k - 1]).Value + ((Number)Tokens[k + 1]).Value;
                        Tokens[k - 1] = new Number(newValue);
                        Tokens.RemoveRange(k, 2);
                    }
                    else
                    {
                        k++;
                    }
                }

                // we only have multiplications left
                return Tokens.OfType<Number>().Select(n => n.Value)
                    .Aggregate((a, b) => a * b);
            }
        }

        static Equation Tokenize(string equation)
        {
            var eq = new Equation();
            for(int n = 0; n < equation.Length; n++)
            {
                var c = equation[n];
                if (char.IsDigit(c)) eq.Tokens.Add(new Number(c - '0'));
                else if (c == '*') eq.Tokens.Add(new Multiply());
                else if (c == '+') eq.Tokens.Add(new Add());
                else if (c == '(')
                {
                    var subEq = ExtractBetweenMatchingBrackets(equation, n);
                    eq.Tokens.Add(Tokenize(subEq));
                    n += subEq.Length;
                }
            }
            return eq;
        }

        static string ExtractBetweenMatchingBrackets(string s, int startPos)
        {
            // find matching end bracket
            var count = 1;
            for (var k = startPos + 1; k < s.Length; k++)
            {
                if (s[k] == ')')
                {
                    count--;
                    if (count == 0)
                    {
                        var subEquation = s.Substring(startPos + 1, k - startPos - 1);
                        return subEquation;
                    }
                }
                else if (s[k] == '(')
                {
                    count++;
                }
            }
            throw new InvalidOperationException("failed to parse subequation");
        }


        public static long SolveEquation2(string equation)
        {
            var eq = Tokenize(equation);
            return eq.Solve();
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
                    var subEquation = ExtractBetweenMatchingBrackets(equation, n);
                    ApplyAction(SolveEquation(subEquation));
                    n += subEquation.Length;
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
