using System.Collections.Generic;

namespace YesChef_DataLayer.DataClasses
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
        public virtual ICollection<StepRecipeDependancy> StepRecipeDependancies { get; set; } = new List<StepRecipeDependancy>();
    }
}
