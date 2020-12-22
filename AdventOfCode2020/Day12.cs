using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day12 : ISolver
    {
        public (string, string) ExpectedResult => ("1589", "23960");

        public (string, string) Solve(string[] input)
        {
            var (x,y,dir) = input.Aggregate((0, 0, 90), Move);
            var part1 = Math.Abs(x) + Math.Abs(y);
            var ((sx, sy), (wx,wy)) = input.Aggregate(((0, 0), (10, 1)), Move2);
            var part2 = Math.Abs(sx) + Math.Abs(sy);
            return (part1.ToString(), part2.ToString());
        }

        public static (int,int,int) Move((int,int,int) currentState, string instruction)
        {
            var (x, y, dir) = currentState;
            var amount = int.Parse(instruction[1..]);
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

        public static (int,int) Rotate((int,int) pos, int angle)
        {
            var (px, py) = pos;

            switch (angle)
            {
                case 90:
                    return (py, 0 - px);
                case 180:
                    return (0 - px, 0 - py);
                case 270:
                    return (0 - py, px);

            }
            throw new InvalidOperationException("Unknown angle");
        }

        public static ((int, int), (int, int)) Move2(((int, int), (int,int)) currentState, string instruction)
        {
            var ((x, y), (wx, wy)) = currentState;
            var amount = int.Parse(instruction[1..]);
            var action = instruction[0];
            switch (action)
            {
                case 'F':
                    x += wx * amount;
                    y += wy * amount;
                    break;
                case 'N':
                    wy += amount;
                    break;
                case 'S':
                    wy -= amount;
                    break;
                case 'E':
                    wx += amount;
                    break;
                case 'W':
                    wx -= amount;
                    break;
                case 'L':
                    (wx, wy) = Rotate((wx, wy), 360-amount);
                    break;
                case 'R':
                    (wx, wy) = Rotate((wx, wy), amount);
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized action");
            }
            return ((x, y), (wx, wy));
        }
    }
}
