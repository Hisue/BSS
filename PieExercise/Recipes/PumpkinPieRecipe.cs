using PieExercise.Ingredients;
using PieExercise.Pies;

namespace PieExercise.Recipes
{
    public class PumpkinPieRecipe : PieRecipe
    {
        public PumpkinPieRecipe(IIngredient flavor, IIngredient egg, IIngredient milk, IIngredient sugar) : base(egg, milk, sugar)
        {
            PieIngredients.Add(flavor);
        }

        public override Pie CreatePie()
        {
            return new PumpkinPie();
        }
    }
}
