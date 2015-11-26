using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer.DataClasses
{
    public class RecipeInstance
    {
        public RecipeInstance()
        {
            RecipeInstanceSteps = new List<RecipeInstanceStep>();
        }
        public int Id { get; set; }

        public int MealId { get; set; }
        [ForeignKey("MealId")]
        public Meal Meal { get; set; }

        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        public virtual ICollection<RecipeInstanceStep> RecipeInstanceSteps { get; set; }
    }
}
