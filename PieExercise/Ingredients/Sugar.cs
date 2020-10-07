namespace PieExercise.Ingredients
{
    public class Sugar : IIngredient
    {
        public int Amount { get; set; }
        public Sugar(int amount)
        {
            Amount = amount;
        }
        public bool IsSameType(object obj)
        {
            return obj is Sugar;
        }
    }
}
