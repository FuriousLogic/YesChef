namespace YesChef_DataLayer.DataClasses
{
    public sealed class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Recipe Recipe { get; set; }
        public QuantityType QuantityType { get; set; }
    }
}
