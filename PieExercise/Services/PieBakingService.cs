using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PieExercise.Ingredients;
using PieExercise.Pies;
using PieExercise.Recipes;

namespace PieExercise.Services
{
    public class PieBakingService : IPieBakingService
    {
        private readonly IRecipeService _recipeService;
        public PieBakingService([NotNull]IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public IEnumerable<Pie> BakeMaximumAmountOfPies(List<PieRecipe> pies, List<IIngredient> totalIngredients)
        {
            var bakedPies = new List<Pie>();
            var mostEfficientRecipe = _recipeService.GetMostEfficientPieRecipe(pies, totalIngredients);

            while (mostEfficientRecipe != null)
            {
                var bakedPie = BakePie(mostEfficientRecipe, totalIngredients);
                bakedPies.Add(bakedPie);

                mostEfficientRecipe = _recipeService.GetMostEfficientPieRecipe(pies, totalIngredients);
            }

            return bakedPies;
        }

        private Pie BakePie(EfficientPieRecipe mostEfficientRecipe, List<IIngredient> totalIngredients)
        {
            _recipeService.SubtractIngredients(totalIngredients, mostEfficientRecipe.PieRecipe.PieIngredients);
            return mostEfficientRecipe.PieRecipe.CreatePie();
        }
    }
}
