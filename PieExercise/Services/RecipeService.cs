using System.Collections.Generic;
using System.Linq;
using PieExercise.Ingredients;
using PieExercise.Recipes;

namespace PieExercise.Services
{
    public class RecipeService : IRecipeService
    {
        public EfficientPieRecipe GetMostEfficientPieRecipe(List<PieRecipe> pies, List<IIngredient> totalIngredients)
        {
            return pies.Select(s =>
                    new EfficientPieRecipe { Efficiency = GetMinProduciblePieCount(totalIngredients, s), PieRecipe = s })
                .OrderByDescending(o => o.Efficiency).FirstOrDefault(s => s.Efficiency > 0);
        }

        public void SubtractIngredients(List<IIngredient> totalIngredients, List<IIngredient> pieRecipeIngredients)
        {
            pieRecipeIngredients.ForEach(a =>
                totalIngredients.Where(s => s.IsSameType(a)).ToList()
                    .ForEach(s => s.SubtractAmount(a.Amount)));
        }

        private static int GetMinProduciblePieCount(List<IIngredient> originalIngredients, PieRecipe pieRecipe)
        {
            var minPieCount = int.MaxValue;
            foreach (var ingredient in pieRecipe.PieIngredients)
            {
                var currentIngredient = originalIngredients.FirstOrDefault(s => s.IsSameType(ingredient));
                if (currentIngredient == null)
                {
                    continue;
                }

                minPieCount = GetMinPieCountFromIngredients(currentIngredient.Amount,
                    minPieCount, ingredient.Amount);
            }

            return minPieCount;
        }

        private static int GetMinPieCountFromIngredients(int currentIngredientAmount,
            int minIngredientCount, int ingredientAmount)
        {
            return currentIngredientAmount / ingredientAmount > minIngredientCount
                ? minIngredientCount
                : currentIngredientAmount / ingredientAmount;
        }
    }
}
