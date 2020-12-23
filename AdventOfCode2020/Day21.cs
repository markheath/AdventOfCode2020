using static MoreLinq.Extensions.SkipUntilExtension;
using static MoreLinq.Extensions.ToDelimitedStringExtension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day21 : ISolver
    {
        public (string, string) ExpectedResult => ("2150", "vpzxk,bkgmcsx,qfzv,tjtgbf,rjdqt,hbnf,jspkl,hdcj");

        public class Food
        {
            public HashSet<string> Ingredients { get; }
            public HashSet<string> Allergens { get; }
            public Food(string description)
            {
                var parts = description.Split();
                Ingredients = parts.TakeWhile(i => !i.StartsWith('(')).ToHashSet();
                Allergens = parts
                               .SkipUntil(i => i.StartsWith('('))
                               .Select(a => a.Trim('(', ',', ')', ' '))
                               .ToHashSet();
            }
        }


        public (string, string) Solve(string[] input)
        {
            var food = input.Select(f => new Food(f));
            var allIngredients = food.SelectMany(f => f.Ingredients).ToHashSet();
            var allAllergens = food.SelectMany(f => f.Allergens).ToHashSet();
            var allergenToIngredient = new Dictionary<string, HashSet<string>>();
            foreach (var f in food)
            {
                foreach(var a in f.Allergens)
                {
                    if (allergenToIngredient.ContainsKey(a))
                    {
                        allergenToIngredient[a].IntersectWith(f.Ingredients);
                    }
                    else
                    {
                        allergenToIngredient[a] = f.Ingredients.ToHashSet();
                    }
                }
            }
            
            // solve the allergens
            bool loop;
            var takenFoods = new HashSet<string>();
            var answers = new Dictionary<string,string>();
            do
            {
                loop = false;
                foreach (var (allergen, ingredients) in allergenToIngredient)
                {
                    var diff = new HashSet<string>(ingredients);
                    diff.ExceptWith(takenFoods);
                    if (diff.Count == 1)
                    {
                        var f = diff.Single();
                        takenFoods.Add(f);
                        answers[allergen] = f;
                        loop = true;
                    }
                }
            } while (loop);
             
            var allergenFreeIngredients = allIngredients.Except(allergenToIngredient.SelectMany(kvp => kvp.Value)).ToHashSet();

            // each ingredient contains 0 or 1 allergen

            
            var part1 = food.Sum(f => f.Ingredients.Intersect(allergenFreeIngredients).Count());
            //ingredientToAllergen.Where(kvp => kvp.Value.Count == 0).Select(kvp => kvp.Key).ToList();
            var part2 = answers 
                .OrderBy(s => s.Key).Select(s => s.Value).ToDelimitedString(",");

            return (part1.ToString(),part2);
        }
    }
}
