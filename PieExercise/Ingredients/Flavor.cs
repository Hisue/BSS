namespace PieExercise.Ingredients
{
    public class Flavor : IIngredient
    {
        public int Amount { get; set; }
        public Flavor(int amount)
        {
            Amount = amount;
        }
        public bool IsSameType(object obj)
        {
            return obj is Flavor;
        }
    }
}
