using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day2 : ISolver
    {
		public (string, string) ExpectedResult => ("614", "354");

        public (string, string) Solve(string[] input)
        {
			var valid = 0;
			var valid2 = 0;
			foreach (var s in input)
			{
				/* splitting technique 1 - risky, assumes fixed length
				var from = s[0];
				var to = s[2];
				var letter = s[4];
				var password = s.Substring(7); */

				/* splitting technique 2 - better, but assumes special characters can't be in the values
				s.Split(new[] { " ", "-", ":" }, StringSplitOptions.RemoveEmptyEntries) */

				var match = Regex.Match(s, @"(\d+)-(\d+) (\w): (\w+)");
				var from = int.Parse(match.Groups[1].Value);
				var to = int.Parse(match.Groups[2].Value);
				var letter = match.Groups[3].Value;
				var password = match.Groups[4].Value;
				if (CheckPassword(from, to, letter[0], password)) valid++;
				if (CheckPassword2(from - 1, to - 1, letter[0], password)) valid2++;

			}

			return (valid.ToString(), valid2.ToString());
		}


		bool CheckPassword(int from, int to, char letter, string password)
		{
			var count = password.Count(c => c == letter);
			return count >= from && count <= to;
		}

		bool CheckPassword2(int pos1, int pos2, char letter, string password)
		{
			return password[pos1] == letter ^ password[pos2] == letter;
		}
	}
}
