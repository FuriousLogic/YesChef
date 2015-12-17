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
        public static RecipeInstanceStep CreateRecipeInstanceStep(int recipeInstanceId, int stepId)
        {
            var db = new YesChefContext();
            var recipeInstanceStep = db.RecipeInstanceSteps.Add(new RecipeInstanceStep
            {
                RecipeInstanceId = recipeInstanceId,
                StepId = stepId
            });
            db.SaveChanges();

            return recipeInstanceStep;
        }

        public static RecipeInstance CreateRecipeInstanceSteps(int recipeInstanceId)
        {
            var recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstanceId);
            foreach (var step in recipeInstance.Recipe.Steps)
            {
                CreateRecipeInstanceStep(recipeInstanceId, step.Id);
            }

            return recipeInstance;
        }

        public static RecipeInstanceStep GetRecipeInstanceStep(int recipeInstanceStepId)
        {
            var db = new YesChefContext();
            return GetRecipeInstanceStep(recipeInstanceStepId, db);
        }
        public static RecipeInstanceStep GetRecipeInstanceStep(int recipeInstanceStepId, YesChefContext db)
        {
            return db.RecipeInstanceSteps.Find(recipeInstanceStepId);
        }
    }
}
