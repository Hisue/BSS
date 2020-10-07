namespace PieExercise.Ingredients
{
    public class Apple : IIngredient
    {
        public int Amount { get; set; }
        public Apple(int amount)
        {
            Amount = amount;
        }
        
        public bool IsSameType(object obj)
        {
            return obj is Apple;
        }
    }
}
