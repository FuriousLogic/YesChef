using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class RecipeInstanceStepHandler
    {
        public static RecipeInstanceStep CreateRecipeInstanceStep(RecipeInstance recipeInstance, Step step)
        {
            var db = new YesChefContext();
            var recipeInstanceStep = db.RecipeInstanceSteps.Add(new RecipeInstanceStep
            {
                RecipeInstance = recipeInstance,
                Step = step
            });
            db.SaveChanges();

            return recipeInstanceStep;
        }

        public static RecipeInstance CreateRecipeInstanceSteps(RecipeInstance recipeInstance)
        {
            foreach (var step in recipeInstance.Recipe.Steps)
            {
                CreateRecipeInstanceStep(recipeInstance, step);
            }

            var db = new YesChefContext();
            recipeInstance = db.RecipeInstances.Find(recipeInstance.Id);
            return recipeInstance;
        }
    }
}
