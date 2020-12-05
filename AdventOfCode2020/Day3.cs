using System.Linq;

namespace AdventOfCode2020
{
    public class Day3 : ISolver
    {
		public (string, string) ExpectedResult => ("223", "3517401300");

		long SlopeProduct(string[] map, (int, int)[] slopes) => slopes.Select(s => CountSlope(map, s)).Aggregate((x, y) => x * y);

		long CountSlope(string[] map, (int right, int down) slope)
		{
			var x = 0;
			var y = 0;
			var trees = 0;
			while (y < map.Length)
			{
				if (IsTree(map, x, y)) trees++;
				x += slope.right;
				y += slope.down;
			}
			return trees;
		}

        bool IsTree(string[] map, int x, int y) => map[y][x % map[0].Length] == '#';

        public (string, string) Solve(string[] input)
        {
			var slope = (3, 1);
			var slopes = new[] {
				(1,1),
				(3,1),
				(5,1),
				(7,1),
				(1,2)
			};

			var a = CountSlope(input, slope).ToString();
			var b = SlopeProduct(input, slopes).ToString();
			return (a, b);
		}
    }
}
