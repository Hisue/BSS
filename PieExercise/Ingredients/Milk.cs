namespace PieExercise.Ingredients
{
    public class Milk : IIngredient
    {
        public int Amount { get; set; }
        public Milk(int amount)
        {
            Amount = amount;
        }

        public bool IsSameType(object obj)
        {
            return obj is Milk;
        }
    }
}
