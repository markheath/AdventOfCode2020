using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4 : ISolver
    {
        public (string, string) ExpectedResult => ("204", "179");
		private static readonly string[] expectedFields = "byr;iyr;eyr;hgt;hcl;ecl;pid".Split(';'); // cid not needed

		IEnumerable<Dictionary<string, string>> ParsePassports(IEnumerable<string> lines)
		{
			var currentPassport = new Dictionary<string, string>();
			foreach (var line in lines)
			{
				if (String.IsNullOrWhiteSpace(line))
				{
					yield return currentPassport;
					currentPassport = new Dictionary<string, string>();
				}
				else
				{
					foreach (var property in line.Split(' '))
					{
						var bits = property.Split(':');

						currentPassport[bits[0]] = bits[1];
					}
				}
			}
			yield return currentPassport;
		}

		bool IsPassportValid(Dictionary<string, string> passport)
		{
			return expectedFields.All(p => passport.ContainsKey(p));
		}
		bool IsPassportValid2(Dictionary<string, string> passport)
		{
			var hasAllFields = expectedFields.All(p => passport.ContainsKey(p));
			if (!hasAllFields) return false;
			var birthYear = Int32.Parse(passport["byr"]);
			var issueYear = Int32.Parse(passport["iyr"]);
			var expirationYear = Int32.Parse(passport["eyr"]);
			var height = passport["hgt"];
			var hairColor = passport["hcl"];
			var eyeColor = passport["ecl"];
			var passportId = passport["pid"];

			if (birthYear < 1920 || birthYear > 2002) return false;
			if (issueYear < 2010 || issueYear > 2020) return false;
			if (expirationYear < 2020 || expirationYear > 2030) return false;
			if (!Regex.IsMatch(hairColor, "^#[0-9a-f]{6}$")) return false;
			if (!Regex.IsMatch(eyeColor, "^amb|blu|brn|gry|grn|hzl|oth$")) return false;
			if (!Regex.IsMatch(passportId, "^[0-9]{9}$")) return false;
			if (height.EndsWith("cm"))
			{
				if (int.TryParse(height.Substring(0, height.Length - 2), out var cm))
				{
					if (cm < 150 || cm > 193) return false;
				}
				else
				{
					return false;
				}
			}
			else if (height.EndsWith("in"))
			{
				if (int.TryParse(height.Substring(0, height.Length - 2), out var inches))
				{
					if (inches < 59 || inches > 76) return false;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
			return true;

		}

		public (string, string) Solve(string[] input)
        {
			var a = ParsePassports(input).Count(IsPassportValid).ToString();
			var b = ParsePassports(input).Count(IsPassportValid2).ToString();
			return (a, b);
		}
	}
}
