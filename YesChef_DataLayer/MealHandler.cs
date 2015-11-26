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
        public static Meal CreateMeal(string name, SousChef sousChef)
        {
            var db = new YesChefContext();
            var meal = db.Meals.Add(new Meal
            {
                CreatedAt = DateTime.Now,
                Name = name,
                SousChef = sousChef
            });
            db.SaveChanges();

            return meal;
        }

        public static Meal CreateMeal(string name, SousChef sousChef, Recipe recipe)
        {
            var meal = CreateMeal(name, sousChef);
            RecipeInstanceHandler.CreateRecipeInstance(recipe, meal);
            return meal;
        }

        public static Meal AddRecipe(Meal meal, Recipe recipe)
        {
            RecipeInstanceHandler.CreateRecipeInstance(recipe, meal);
            return meal;
        }
    }
}
