using System;
using System.Collections.Generic;
using System.Linq;
using PieExercise.Ingredients;
using PieExercise.Pies;
using PieExercise.Recipes;
using PieExercise.Services;

namespace PieExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            const string input = "12,4,40,30,40";
            //const string input = "10,14,10,42,24";
            var parsedValues = input.Split(',').Select(int.Parse).ToArray();

            if (parsedValues.Length != 5)
            {
                throw new ArgumentException("Wrong input");
            }

            var totalIng = new List<IIngredient>
            {
                new Flavor(parsedValues[0]),
                new Apple(parsedValues[1]),
                new Egg(parsedValues[2]),
                new Milk(parsedValues[3]),
                new Sugar(parsedValues[4])
            };

            var pumpkinPieRecipe = new PumpkinPieRecipe(new Flavor(1), new Egg(3), new Milk(4), new Sugar(3));
            var applePieRecipe = new ApplePieRecipe(new Apple(1), new Egg(4), new Milk(3), new Sugar(2));

            var pieRecipes = new List<PieRecipe>
            {
                pumpkinPieRecipe,
                applePieRecipe
            };

            var bakingService = new PieBakingService(new RecipeService());

            var bakedPies = bakingService.BakeMaximumAmountOfPies(pieRecipes, totalIng).ToList();

            Console.WriteLine($"PumpkinPie: {bakedPies.Count(s => s is PumpkinPie)}");
            Console.WriteLine($"ApplePie: {bakedPies.Count(s => s is ApplePie)}");
        }
    }
}
