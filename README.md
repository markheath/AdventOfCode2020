# Advent of Code 2020 

This project contains solutions to the [Advent of Code](https://adventofcode.com/) puzzles for 2020 in C#.

In previous years I have used these puzzles to improve my skills in other languages (F#, JavaScript), or to apply functional programming principles.

This year I am using it to help one of my children with his programming skills. He's solving them in Python and I'm letting him watch me solve them in C#. So the solutions this year aren't necessarily going to be very "clever" in terms of doing things in the shortest way. Instad I'm focusing on making the coding techniques understandable for a beginner (although refactoring some of the answers afterwards to show more advanced techniques).

Note - this does mean I might not complete this year's puzzles. Advent of Code does have a habit of getting quite tricky by the end!

Some notes on each day's solution.

- **Day 1** presented an ideal opportunity to use [MoreLinq's Subsets method](https://markheath.net/post/exploring-morelinq-4-combinations), and the whole solution is easily achieved in a single LINQ expression by also using `Aggregate` to multiply the terms together. One trick here was to remember to use `long` instead of `int` to avoid an overflow.
- **Day 2** gave me a chance to introduce my son to Regular Expressions. I stared by showing some ways we could parse the input line just by string splitting, and then we saw how Regex can help. Personally I find C#'s Regex class a bit fiddly to work with.
- **Day 3** gave a chance to introduce the concept of Tuples and also put modulo arithmetic to use. It's another one that could be turned nicely into a single LINQ query, with MoreLinq's Scan method probably being a good candidate, but I wanted to keep it understandable. It also showed the value of breaking the solution up into a few smaller helper functions, which could be tested individually.
- **Day 4** again highlighted the power of Regex, and there were some other interesting topics this puzzle raised. First was the need to batch input rows together (which again MoreLinq could have helped with), but I did more simply with the yield statement. The trick is not to accidentally forget the first batch. The second issue is whether this problem deserves it's own `Passport` C# class, allowing it to own its own validation logic, which would be a better approach in an enterprise application, rather than just using a `Dictionary<string,string>` to hold the state. The code for this day certainly could do with some cleaning up!
- **Day 5** I started out writing some long-hand code with a switch statement to perform the binary split. This was a pain to get right as it's easy to get off by one errors when chopping the range in half. But a eureka moment ensued when I realised the input was just a binary number and a bit of string replacement took me straight to the answer. This was also a good opportunity to introduce concept of `HashSet`, and to use a parameterized unit test.
