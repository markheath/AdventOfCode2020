namespace AdventOfCode2020
{
    public interface ISolver
    {
        public (string Part1, string Part2) Solve(string[] input);
        public (string Part1, string Part2) ExpectedResult { get; }
    }
}
