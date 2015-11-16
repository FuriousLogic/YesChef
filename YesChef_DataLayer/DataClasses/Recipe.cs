using System.Collections.Generic;

namespace YesChef_DataClasses
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
    }
}
