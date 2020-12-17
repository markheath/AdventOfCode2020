using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class ConwayCube
    {
        private HashSet<(int, int, int, int)> occupiedCells = new HashSet<(int, int, int, int)>();
        private int minX;
        private int minY;
        private int minZ;
        private int minW;
        private int maxX;
        private int maxY;
        private int maxZ;
        private int maxW;

        public int ActiveCells { get => occupiedCells.Count; }

        public ConwayCube(string[] startState)
            : this(ParseStartState(startState))
        {
        }

        public ConwayCube(IEnumerable<(int,int,int,int)> occupiedCells)
        {
            foreach (var c in occupiedCells) Add(c);
        }

        public static IEnumerable<(int,int,int,int)> ParseStartState(string[] startState)
        {
            return
            from y in Enumerable.Range(0, startState.Length)
            from x in Enumerable.Range(0, startState[y].Length)
            where startState[y][x] == '#'
            select (x, y, 0, 0);
        }

        public IEnumerable<(int, int, int, int)> AllMutatableCells(int dim)
        {
            return
            from x in Enumerable.Range(minX - 1, maxX - minX + 3)
            from y in Enumerable.Range(minY - 1, maxY - minY + 3)
            from z in Enumerable.Range(minZ - 1, maxZ - minZ + 3)
            from w in dim == 4 ? Enumerable.Range(minW - 1, maxW - minW + 3) : new[] { 0 }
            select (x, y, z, w);
        }

        public IEnumerable<(int, int, int, int)> AllAdjacentCells((int, int, int, int) cell,int dim)
        {
            var (x, y, z, w) = cell;
            return
            from dx in Enumerable.Range(-1, 3)
            from dy in Enumerable.Range(-1, 3)
            from dz in Enumerable.Range(-1, 3)
            from dw in dim == 4 ? Enumerable.Range(-1, 3) : new[] { 0 }
            where !((dx == 0) && (dy == 0) && (dz == 0) && (dw == 0))
            select (x + dx, y + dy, z + dz, w + dw);
        }

        public bool GetNextState((int, int, int, int) cell, int dim)
        {
            var currentState = occupiedCells.Contains(cell);
            var adjacent = AllAdjacentCells(cell, dim)
                .Where(c => occupiedCells.Contains(c))
                .Take(4).Count(); // optimize because highest adjacent count we need is 3
            if (currentState)
                return adjacent == 2 || adjacent == 3;
            return adjacent == 3;
        }

        public void Add((int,int,int,int) cell)
        {
            var (x, y, z, w) = cell;
            if (x < minX) minX = x;
            if (y < minY) minY = y;
            if (z < minZ) minZ = z;
            if (w < minW) minW = w;
            if (x > maxX) maxX = x;
            if (y > maxY) maxY = y;
            if (z > maxZ) maxZ = z;
            if (w > maxW) maxW = w;
            occupiedCells.Add(cell);
        }

        public ConwayCube Mutate(int dim)
        {
            return new ConwayCube(AllMutatableCells(dim).Where(x => GetNextState(x,dim)));     
        }
    }


    public class Day17 : ISolver
    {
        public (string, string) ExpectedResult => ("353", "2472");

        public (string, string) Solve(string[] input)
        {
            var cube = new ConwayCube(input);
            for(var n = 0; n < 6; n++) cube = cube.Mutate(3);
            var part1 = cube.ActiveCells;

            cube = new ConwayCube(input);
            for (var n = 0; n < 6; n++) cube = cube.Mutate(4);
            var part2 = cube.ActiveCells;

            return (part1.ToString(), part2.ToString());
        }
    }
}
