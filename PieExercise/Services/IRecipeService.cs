using System.Collections.Generic;
using PieExercise.Ingredients;
using PieExercise.Recipes;

namespace PieExercise.Services
{
    public interface IRecipeService
    {
        EfficientPieRecipe GetMostEfficientPieRecipe(List<PieRecipe> pies, List<IIngredient> total);
        void SubtractIngredients(List<IIngredient> total, List<IIngredient> pieRecipe);
    }
}
