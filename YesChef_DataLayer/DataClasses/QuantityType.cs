using System.Collections.Generic;

namespace YesChef_DataLayer.DataClasses
{
    public class QuantityType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
