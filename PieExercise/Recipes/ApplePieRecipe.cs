using PieExercise.Ingredients;
using PieExercise.Pies;

namespace PieExercise.Recipes
{
    public class ApplePieRecipe : PieRecipe
    {
        public ApplePieRecipe(IIngredient apple, IIngredient egg, IIngredient milk, IIngredient sugar) : base(egg, milk, sugar)
        {
            PieIngredients.Add(apple);
        }

        public override Pie CreatePie()
        {
            return new ApplePie();
        }
    }
}
