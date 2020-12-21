using static MoreLinq.Extensions.SkipUntilExtension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day21 : ISolver
    {
        public (string, string) ExpectedResult => ("2150", "");

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
            /*
            // solve the allergens
            bool loop;
            var solvedAllergens = new HashSet<string>();
            do
            {
                loop = true;
                foreach (var kvp in allergenToIngredient)
                {
                    var allergen = kvp.Key;
                    var ingredients = kvp.Value;
                    if (ingredients.Count == 1 && !solvedAllergens.Contains(allergen))
                    {
                        solvedAllergens.Add(allergen);
                        foreach(var v in allergenToIngredient.Where(kvp => kvp.Key != allergen))
                        {
                            if (ingredients != v.Value)
                                v.Value.Remove(ingredients.Single());
                        }
                    }
                    else if (ingredients.Count > 1)
                    {
                        loop = true;
                    }
                    else
                    {
                        //throw new InvalidOperationException("oops");
                    }
                }
            } while (loop);*/
             
            var allergenFreeIngredients = allIngredients.Except(allergenToIngredient.SelectMany(kvp => kvp.Value)).ToHashSet();

            // each ingredient contains 0 or 1 allergen

            
            var part1 = food.Sum(f => f.Ingredients.Intersect(allergenFreeIngredients).Count());
            //ingredientToAllergen.Where(kvp => kvp.Value.Count == 0).Select(kvp => kvp.Key).ToList();


            return (part1.ToString(),"");
        }
    }
}
