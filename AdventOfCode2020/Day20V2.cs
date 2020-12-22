using System.Linq;
using System.Collections.Generic;
using System;
using MoreLinq.Extensions;
using System.Diagnostics;

namespace AdventOfCode2020
{
    // based on solution by AlaskanShade with minor changes
    public class Day20 : ISolver
    {
        public (string, string) ExpectedResult => ("79412832860579", "2155"); 
        public (string, string) Solve(string[] input)
        {
            // Parse the tiles
            var tiles = input.Split("").Select(g => new Tile(g.ToArray())).ToArray();

            // The image is a square, so calculate how many tiles wide/high
            var gridSize = (int)Math.Sqrt(tiles.Length);
            // Find the corners
            var corners = tiles.Where(t => t.Matches(tiles) == 2).ToList();
            // We can also find the edges and middle, but it isn't needed
            //var edges = tiles.Where(t => t.Matches(tiles) == 3);
            //var middle = tiles.Where(t => t.Matches(tiles) == 4);

            // For part 1, multiply the corner IDs
            var part1 = corners.Aggregate(1L, (i, v) => i * v.ID).ToString();

            // Generate a lookup by side values so we don't have to regenerate them every time
            var sides = new Dictionary<int, List<Tile>>(
                tiles.SelectMany(t => t.Sides.Select(s => new { s, t }))
                .GroupBy(s => s.s)
                .Where(s => s.Count() > 1)
                .Select(s => new KeyValuePair<int, List<Tile>>(s.Key, s.Select(i => i.t).ToList()))
            );
            // Assemble the pieces starting with the top left corner
            var assembled = new Tile[gridSize, gridSize];
            //Console.WriteLine("{0} {1}", topLeft.GetSide(Tile.SideName.Left), topLeft.GetSide(Tile.SideName.Right));
            for (int c = 0; c < gridSize; c++)
                for (int r = 0; r < gridSize; r++)
                {
                    Tile nextTile;
                    if (c == 0 && r == 0) // Find the corner and orient
                    {
                        nextTile = corners.First();
                        while (!sides.ContainsKey(nextTile.GetSide(Tile.SideName.Right)) || !sides.ContainsKey(nextTile.GetSide(Tile.SideName.Bottom)))
                            nextTile.Rotate();
                    }
                    else if (r == 0) // top row: find the tile that fits to the right of the last one
                    {
                        var lastRight = assembled[r, c - 1].GetSide(Tile.SideName.Right);
                        nextTile = sides[lastRight].Single(e => !e.IsPlaced);
                        // Rotate until the right side is on the left
                        while (nextTile.GetSide(Tile.SideName.Left) != lastRight && nextTile.GetSide(Tile.SideName.LeftReverse) != lastRight)
                            nextTile.Rotate();
                        // Flip if necessary
                        if (nextTile.GetSide(Tile.SideName.Left) != lastRight)
                            nextTile.FlipVertically();
                    }
                    else // find the tile that fits the one above
                    {
                        var lastBottom = assembled[r - 1, c].GetSide(Tile.SideName.Bottom);
                        nextTile = sides[lastBottom].Single(e => !e.IsPlaced);
                        while (nextTile.GetSide(Tile.SideName.Top) != lastBottom && nextTile.GetSide(Tile.SideName.TopReverse) != lastBottom)
                            nextTile.Rotate();
                        if (nextTile.GetSide(Tile.SideName.Top) != lastBottom)
                            nextTile.FlipHorizontally();
                    }
                    assembled[r, c] = nextTile;
                    nextTile.IsPlaced = true;
                }

            // Extract the final image
            var grid = new char[96, 96];
            for (int c = 0; c < gridSize; c++)
                for (int i = 1; i < 9; i++)
                    for (int r = 0; r < gridSize; r++)
                        for (int j = 1; j < 9; j++)
                            grid[c * 8 + i - 1, r * 8 + j - 1] = assembled[r, c].Data[j,i];

            // Count the total water, including potential monsters
            var waterCount = grid.Cast<char>().Count(c => c == '#');

            // Go through each orientation and see if we find monsters
            for (int i = 0; i < 8; i++)
            {
                var count = CountMonsters(grid);
                // Spotted! Subtract 15 for each monster from the total
                if (count > 0) return (part1.ToString(), (waterCount - count * 15).ToString());
                // After the first 4 rotations, do a flip
                if (i == 4) grid = Helpers.FlipH(grid);
                else grid = Helpers.RotateCW(grid);
            }

            throw new InvalidOperationException("Not found");
        }

        private int CountMonsters(char[,] input)
        {
            var cnt = 0;
            for (int c = 0; c < input.GetLength(1) - 20; c++)
                for (int r = 0; r < input.GetLength(0) - 3; r++)
                    if (HasMonster(input, r, c)) cnt++;
            return cnt;
        }

        private bool HasMonster(char[,] grid, int r, int c)
        {
            var monster = new[] { "..................#.",
                                  "#....##....##....###",
                                  ".#..#..#..#..#..#..." };
            // nb r2 and c2 switched?
            for (int c2 = 0; c2 < 20; c2++)
                for (int r2 = 0; r2 < 3; r2++)
                    if (monster[r2][c2] == '#' && grid[c + c2, r + r2] != '#') return false;
            return true;
        }

        [DebuggerDisplay("{ID}")]
        private class Tile
        {
            public enum SideName { Top, TopReverse, Left, LeftReverse, Bottom, BottomReverse, Right, RightReverse }
            public int ID { get; private set; }
            public char[,] Data { get; private set; }
            public bool IsPlaced { get; set; }
            public IEnumerable<int> Sides => Enum.GetValues(typeof(SideName)).Cast<SideName>().Select(n => GetSide(n));

            public Tile(string[] lines)
            {
                ID = int.Parse(lines[0].Substring(5).TrimEnd(':'));
                var d = lines.Skip(1).ToArray();
                Data = new char[d.Length, d[0].Length];
                for (var r = 0; r < d.Length; r++)
                    for (var c = 0; c < d[r].Length; c++)
                        Data[r, c] = d[r][c];
            }

            public int GetSide(SideName name)
            {
                switch (name)
                {
                    case SideName.Top:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[0,r]).ToArray()));
                    case SideName.TopReverse:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[0,r]).Reverse().ToArray()));
                    case SideName.Left:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[r,0]).ToArray()));
                    case SideName.LeftReverse:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[r,0]).Reverse().ToArray()));
                    case SideName.Bottom:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[9,r]).ToArray()));
                    case SideName.BottomReverse:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[9,r]).Reverse().ToArray()));
                    case SideName.Right:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[r,9]).ToArray()));
                    case SideName.RightReverse:
                        return ParseNumber(new String(Enumerable.Range(0, 10).Select(r => Data[r,9]).Reverse().ToArray()));
                }
                return -1;
            }

            public void Rotate()
            {
                Data = Helpers.RotateCW(Data);
            }

            public void FlipHorizontally()
            {
                Data = Helpers.FlipH(Data);
            }

            public void FlipVertically()
            {
                Data = Helpers.FlipV(Data);
            }

            public int Matches(IEnumerable<Tile> tiles)
            {
                return tiles.Count(t => t.ID != ID && t.Sides.Any(s => Sides.Contains(s)));
            }

            private int ParseNumber(string text)
            {
                return Convert.ToInt32(text.Replace('#', '1').Replace('.', '0'), 2);
            }
        }

        static class Helpers
        {
            public static char[,] FlipV(char[,] data)
            {
                var n = data.GetLength(0);
                var updated = new char[data.GetLength(0), data.GetLength(1)];
                for (var r = 0; r < data.GetLength(0); r++)
                    for (var c = 0; c < data.GetLength(1); c++)
                        updated[n - 1 - r, c] = data[r, c];
                return updated;
            }
            public static char[,] FlipH(char[,] data)
            {
                var n = data.GetLength(0);
                var updated = new char[data.GetLength(0), data.GetLength(1)];
                for (var r = 0; r < data.GetLength(0); r++)
                    for (var c = 0; c < data.GetLength(1); c++)
                        updated[r, n - 1 - c] = data[r, c];
                return updated;

            }
            public static char[,] RotateCW(char[,] data)
            {
                var n = data.GetLength(0);
                var rotated = new char[data.GetLength(0), data.GetLength(1)];
                for (var r = 0; r < data.GetLength(0); r++)
                    for (var c = 0; c < data.GetLength(1); c++)
                        rotated[c,n - 1 - r] = data[r,c];
                return rotated;
            }
        }
    }
}
