using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class RecipeInstanceHandler
    {
        public static RecipeInstance CreateRecipeInstance(Recipe recipe, Meal meal)
        {
            var db = new YesChefContext();
            var recipeInstance = db.RecipeInstances.Add(new RecipeInstance
            {
                Meal = meal,
                Recipe = recipe
            });
            db.SaveChanges();

            return recipeInstance;
        }
    }
}
