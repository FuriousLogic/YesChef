using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class RecipeHandler
    {
        public static Recipe CreateRecipe(string name)
        {
            var db = new YesChefContext();
            var recipe = db.Recipies.Add(new Recipe { Name = name });
            db.SaveChanges();

            return recipe;
        }

        public static List<Recipe> GetAllRecipies()
        {
            var db = new YesChefContext();
            return db.Recipies.ToList();
        }

        public static int GetRecipeBusyTime(int recipeId)
        {
            var recipe = GetRecipe(recipeId);
            return recipe.Steps.Where(step => !step.IsFreeTime).Sum(step => step.MinutesDuration);
        }

        public static int GetRecipeFreeTime(int recipeId)
        {
            var recipe = GetRecipe(recipeId);
            return recipe.Steps.Where(step => step.IsFreeTime).Sum(step => step.MinutesDuration);
            //return recipe.StepRecipeDependancies.Where(step => step.IsFreeTime).Sum(step => step.MinutesDuration);
        }

        public static Recipe GetRecipe(int recipeId)
        {
            var db = new YesChefContext();
            var recipe = db.Recipies.Find(recipeId);
            return recipe;
        }
    }
}
