namespace PieExercise.Ingredients
{
    public class Egg : IIngredient
    {
        public int Amount { get; set; }
        public Egg(int amount)
        {
            Amount = amount;
        }
        public bool IsSameType(object obj)
        {
            return obj is Egg;
        }
    }
}
