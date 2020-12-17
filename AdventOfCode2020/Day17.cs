using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class ConwayCube
    {
        private HashSet<(int, int, int)> occupiedCells = new HashSet<(int, int, int)>();
        private int minX;
        private int minY;
        private int minZ;
        private int maxX;
        private int maxY;
        private int maxZ;

        public int ActiveCells { get => occupiedCells.Count; }

        public ConwayCube(string[] startState)
            : this(ParseStartState(startState))
        {
        }

        public ConwayCube(IEnumerable<(int,int,int)> occupiedCells)
        {
            foreach (var c in occupiedCells) Add(c);
        }

        public static IEnumerable<(int,int,int)> ParseStartState(string[] startState)
        {
            return
            from y in Enumerable.Range(0, startState.Length)
            from x in Enumerable.Range(0, startState[y].Length)
            where startState[y][x] == '#'
            select (x, y, 0);
        }

        public IEnumerable<(int,int,int)> AllMutatableCells()
        {
            return
            from x in Enumerable.Range(minX - 1, maxX - minX + 3)
            from y in Enumerable.Range(minY - 1, maxY - minY + 3)
            from z in Enumerable.Range(minZ - 1, maxZ - minZ + 3)
            select (x, y, z);
        }

        public IEnumerable<(int, int, int)> AllAdjacentCells((int, int, int) cell)
        {
            var (x, y, z) = cell;
            return
            from dx in Enumerable.Range(-1, 3)
            from dy in Enumerable.Range(-1, 3)
            from dz in Enumerable.Range(-1, 3)
            where !((dx == 0) && (dy == 0) && (dz == 0))
            select (x + dx, y + dy, z + dz);
        }

        public bool GetNextState((int,int,int) cell)
        {
            var currentState = occupiedCells.Contains(cell);
            var adjacent = AllAdjacentCells(cell).Count(c => occupiedCells.Contains(c));
            if (currentState)
                return adjacent == 2 || adjacent == 3;
            return adjacent == 3;
        }

        public void Add((int,int,int) cell)
        {
            var (x, y, z) = cell;
            if (x < minX) minX = x;
            if (y < minY) minY = y;
            if (z < minZ) minZ = z;
            if (x > maxX) maxX = x;
            if (y > maxY) maxY = y;
            if (z > maxZ) maxZ = z;
            occupiedCells.Add(cell);
        }

        public ConwayCube Mutate()
        {
            return new ConwayCube(AllMutatableCells().Where(GetNextState));
            
        }
    }


    public class Day17 : ISolver
    {

        public (string, string) ExpectedResult => ("353", "");

        public (string, string) Solve(string[] input)
        {
            var cube = new ConwayCube(input);
            for(var n = 0; n < 6; n++) cube = cube.Mutate();
            var part1 = cube.ActiveCells;
            return (part1.ToString(), "");
        }
    }
}
