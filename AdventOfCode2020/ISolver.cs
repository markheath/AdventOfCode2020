namespace AdventOfCode2020
{
    public interface ISolver
    {
        public (string,string) Solve(string[] input);
        public (string, string) ExpectedResult { get; }
    }
}
