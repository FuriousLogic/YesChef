using System;
using System.Collections.Generic;
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
        public Recipe Recipe { get; set; }
        public Meal Meal { get; set; }

        public ICollection<RecipeInstanceStep> RecipeInstanceSteps { get; set; }
    }
}
