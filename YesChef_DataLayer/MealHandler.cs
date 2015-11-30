using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class MealHandler
    {
        public static Meal CreateMeal(string name, int sousChefId)
        {
            var db = new YesChefContext();
            var meal = db.Meals.Add(new Meal
            {
                CreatedAt = DateTime.Now,
                Name = name,
                SousChefId = sousChefId
            });
            db.SaveChanges();

            return GetMeal(meal.Id);
        }

        public static Meal CreateMeal(string name, int sousChefId, int recipeId)
        {
            var meal = CreateMeal(name, sousChefId);
            RecipeInstanceHandler.CreateRecipeInstance(recipeId, meal.Id);
            return GetMeal(meal.Id);
        }

        public static Meal AddRecipe(int mealId, int recipeId)
        {
            RecipeInstanceHandler.CreateRecipeInstance(recipeId, mealId);
            return GetMeal(mealId);
        }

        public static Meal GetMeal(int mealId)
        {
            var db = new YesChefContext();
            var meal = db.Meals.Single(m => m.Id == mealId);
            return meal;
        }
    }
}
