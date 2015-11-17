using System;
using System.Collections.Generic;
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
    }
}
