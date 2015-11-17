using System.Collections.Generic;

namespace YesChef_DataLayer.DataClasses
{
    public sealed class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Step> Steps { get; set; }
    }
}
