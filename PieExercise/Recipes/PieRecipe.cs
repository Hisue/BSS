using System.Collections.Generic;
using PieExercise.Ingredients;
using PieExercise.Pies;

namespace PieExercise.Recipes
{
    public abstract class PieRecipe
    {
        public List<IIngredient> PieIngredients { get; set; } = new List<IIngredient>();

        protected PieRecipe(IIngredient egg, IIngredient milk, IIngredient sugar)
        {
            PieIngredients.Add(egg);
            PieIngredients.Add(milk);
            PieIngredients.Add(sugar);
        }

        public abstract Pie CreatePie();
    }
}
