using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class RecipeInstanceHandler
    {
        public static RecipeInstance CreateRecipeInstance(int recipeId, int mealId)
        {
            var db = new YesChefContext();
            var recipeInstance = db.RecipeInstances.Add(new RecipeInstance
            {
                MealId = mealId,
                RecipeId = recipeId
            });
            db.SaveChanges();

            recipeInstance = GetRecipeInstance(recipeInstance.Id);
            return recipeInstance;
        }

        public static RecipeInstance GetRecipeInstance(int recipeInstanceId)
        {
            var db = new YesChefContext();
            var recipeInstance = db.RecipeInstances
                //.Include(ri => ri.)
                .Single(ri => ri.Id == recipeInstanceId);
            return recipeInstance;
        }
    }
}
