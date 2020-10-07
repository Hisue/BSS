using System.Collections.Generic;
using PieExercise.Ingredients;
using PieExercise.Pies;
using PieExercise.Recipes;

namespace PieExercise.Services
{
    internal interface IPieBakingService
    {
        IEnumerable<Pie> BakeMaximumAmountOfPies(List<PieRecipe> pies, List<IIngredient> totalIngredients);
    }
}
