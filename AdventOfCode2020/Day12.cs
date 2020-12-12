using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day12 : ISolver
    {
        public (string, string) ExpectedResult => ("1589", "");

        public (string, string) Solve(string[] input)
        {
            var (x,y,dir) = input.Aggregate((0, 0, 90), Move);
            var part1 = Math.Abs( x) + Math.Abs(y);
            return (part1.ToString(), "");
        }

        public static (int,int,int) Move((int,int,int) currentState, string instruction)
        {
            var (x, y, dir) = currentState;
            var amount = int.Parse(instruction.Substring(1));
            var action = instruction[0];
            if (action == 'F')
            {
                switch (dir)
                {
                    case 0:
                        action = 'N';
                        break;
                    case 90:
                        action = 'E';
                        break;
                    case 180: 
                        action = 'S';
                        break;
                    case 270:
                        action = 'W';
                        break;
                    default:
                        throw new InvalidOperationException("Invalid direction");
                }
            }
            switch (action)
            {
                case 'N':
                    y -= amount;
                    break;
                case 'S':
                    y += amount;
                    break;
                case 'E':
                    x += amount;
                    break;
                case 'W':
                    x -= amount;
                    break;
                case 'L':
                    dir -= amount;
                    break;
                case 'R':
                    dir += amount;
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized action");
            }
            if (dir < 0) dir += 360;
            return (x, y, dir % 360);
        }
    }
}
