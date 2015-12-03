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

            var recipe = RecipeHandler.GetRecipe(recipeId);
            foreach (var step in recipe.Steps)
            {
                recipeInstance.RecipeInstanceSteps.Add(new RecipeInstanceStep()
                {
                    StepId = step.Id
                });
            }

            db.SaveChanges();

            recipeInstance = GetRecipeInstance(recipeInstance.Id);
            return recipeInstance;
        }

        public static RecipeInstance GetRecipeInstance(int recipeInstanceId)
        {
            var db = new YesChefContext();
            var recipeInstance = db.RecipeInstances
                .Single(ri => ri.Id == recipeInstanceId);
            return recipeInstance;
        }

        private static void FillTimings(ICollection<RecipeInstanceStep> recipeInstanceSteps)
        {
            while (true)
            {
                //Get number of unscheduled steps
                var unscheduledCount = (from ris in recipeInstanceSteps where ris.MinutesBeforeRecipeEndToStart==0 select ris).ToList().Count;

                foreach (var recipeInstanceStep in recipeInstanceSteps)
                {
                    //Is it scheduled already?
                    if (recipeInstanceStep.MinutesBeforeRecipeEndToStart > 0) continue;

                    //Is it an unscheduled terminator step?
                    var childSteps = StepHandler.GetChildSteps(recipeInstanceStep.StepId);
                    if (childSteps.Count == 0)
                    {
                        recipeInstanceStep.MinutesBeforeRecipeEndToStart = recipeInstanceStep.Step.MinutesDuration;
                        continue;
                    }

                    //Are all of it's children scheduled?
                    var maxMinutesStartForChild = 0;
                    foreach (var childStep in childSteps)
                    {
                        var instanceStep = recipeInstanceSteps.Single(ris => ris.StepId == childStep.Id);
                        if (instanceStep.MinutesBeforeRecipeEndToStart > 0)
                        {
                            if (instanceStep.MinutesBeforeRecipeEndToStart > maxMinutesStartForChild)
                                maxMinutesStartForChild = instanceStep.MinutesBeforeRecipeEndToStart;
                        }
                    }
                    if (maxMinutesStartForChild > 0)
                        recipeInstanceStep.MinutesBeforeRecipeEndToStart = maxMinutesStartForChild +
                                                                           recipeInstanceStep.Step.MinutesDuration;
                }

                //All Done?
                var instanceStepsNotScheduled = (from ris in recipeInstanceSteps where ris.MinutesBeforeRecipeEndToStart<=0 select ris).ToList();
                if(instanceStepsNotScheduled.Count==0)
                    break;

                //Check number of unscheduled steps
                if( unscheduledCount <= (from ris in recipeInstanceSteps where ris.MinutesBeforeRecipeEndToStart == 0 select ris).ToList().Count)
                    throw new Exception("Scheduling Error");


            }
        }

        public static int GetMinutesToFinish(int recipeInstanceId)
        {
            //Find all end points for recipe steps
            var recipeInstance = GetRecipeInstance(recipeInstanceId);
            var recipeInstanceSteps = recipeInstance.RecipeInstanceSteps;
            FillTimings(recipeInstanceSteps);

            var longestTime = recipeInstanceSteps.Max(x=>x.MinutesBeforeRecipeEndToStart);

            return longestTime;
        }



    }
}
