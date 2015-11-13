namespace YesChef_DataClasses
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual QuantityType QuantityType { get; set; }
    }
}
