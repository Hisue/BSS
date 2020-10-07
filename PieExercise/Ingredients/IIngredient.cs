namespace PieExercise.Ingredients
{
    public interface IIngredient : IComparable
    {
        int Amount { get; set; }

        public void SubtractAmount(int amount)
        {
            Amount -= amount;
        }
    }
}
