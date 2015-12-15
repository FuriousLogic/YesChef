using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class RecipeInstanceHandler
    {
        public static RecipeInstance CreateRecipeInstance(int recipeId, int mealId)
        {
            var db = new YesChefContext();
            var recipeInstance = db.RecipeInstances.Add(new RecipeInstance
            {
                MealId = mealId,
                RecipeId = recipeId
            });

            //Add steps from recipe
            var recipe = RecipeHandler.GetRecipe(recipeId);
            foreach (var step in recipe.Steps)
            {
                recipeInstance.RecipeInstanceSteps.Add(new RecipeInstanceStep()
                {
                    StepId = step.Id
                });

                //Add steps from connected recipies
                var parentRecipe = (from x in db.StepRecipeDependancies where x.StepId==step.Id select x.Recipe).SingleOrDefault();
                if (parentRecipe != null)
                {
                    foreach (var parentRecipeStep in parentRecipe.Steps)
                    {
                        recipeInstance.RecipeInstanceSteps.Add(new RecipeInstanceStep()
                        {
                            StepId = parentRecipeStep.Id
                        });
                    }
                }
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

        private static void FillTimings(ICollection<RecipeInstanceStep> uncompletedRecipeInstanceSteps)
        {
            while (true)
            {
                //Get number of unscheduled steps
                var unscheduledCount = (from ris in uncompletedRecipeInstanceSteps where ris.MinutesBeforeRecipeEndToStart == 0 select ris).ToList().Count;

                foreach (var recipeInstanceStep in uncompletedRecipeInstanceSteps)
                {
                    //todo:
                    //***NEEDS TO RECOGNISE STEP-RECIPE LINK***
                    //*** alter GetChildSteps! Needs to recognise final step may go to other recipe

                    //Is it scheduled already?
                    if (recipeInstanceStep.MinutesBeforeRecipeEndToStart > 0) continue;

                    //Is it a terminator step and not a parent recipe?
                    var childSteps = StepHandler.GetChildSteps(recipeInstanceStep.Id);
                    if (childSteps.Count == 0 && recipeInstanceStep.Step.Recipe.StepRecipeDependancies.Count==0)
                    {
                        recipeInstanceStep.MinutesBeforeRecipeEndToStart = recipeInstanceStep.Step.MinutesDuration;
                        continue;
                    }

                    //Are all of it's children scheduled?
                    var maxMinutesStartForChild = 0;
                    foreach (var childStep in childSteps)
                    {
                        var instanceStep = uncompletedRecipeInstanceSteps.Single(ris => ris.StepId == childStep.Id);
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
                var instanceStepsNotScheduled = (from ris in uncompletedRecipeInstanceSteps where ris.MinutesBeforeRecipeEndToStart <= 0 select ris).ToList();
                if (instanceStepsNotScheduled.Count == 0)
                    break;

                //Check number of unscheduled steps
                if (unscheduledCount <= (from ris in uncompletedRecipeInstanceSteps where ris.MinutesBeforeRecipeEndToStart == 0 select ris).ToList().Count)
                    throw new Exception("Scheduling Error");

            }
        }

        public static int GetMinutesToFinish(int recipeInstanceId)
        {
            //Find all end points for recipe steps
            var recipeInstance = GetRecipeInstance(recipeInstanceId);
            var recipeInstanceSteps = recipeInstance.RecipeInstanceSteps.Where(ris => ris.Finished == null).ToList();
            FillTimings(recipeInstanceSteps);

            var longestTime = recipeInstanceSteps.Max(x => x.MinutesBeforeRecipeEndToStart);

            return longestTime;
        }


        public static RecipeInstanceStep YesChef(int recipeInstanceId, int stepId)
        {
            var db = new YesChefContext();
            var recipeInstanceStep = (from ris in db.RecipeInstanceSteps where ris.RecipeInstanceId == recipeInstanceId && ris.StepId == stepId select ris).FirstOrDefault();
            if (recipeInstanceStep == null) throw new Exception("Cannot find recipeInstanceStep");

            recipeInstanceStep.Started = DateTime.Now;
            db.SaveChanges();

            return recipeInstanceStep;
        }

        public static RecipeInstanceStep FinishedChef(int recipeInstanceId, int stepId)
        {
            var db = new YesChefContext();
            var recipeInstanceStep = (from ris in db.RecipeInstanceSteps where ris.RecipeInstanceId == recipeInstanceId && ris.StepId == stepId select ris).FirstOrDefault();
            if (recipeInstanceStep == null) throw new Exception("Cannot find recipeInstanceStep");

            //Make sure it's started
            if (recipeInstanceStep.Started == null)
                throw new Exception("Step not yet started!");

            recipeInstanceStep.Finished = DateTime.Now;

            //can the recipe be marked as completed?
            var uncompletedRecipeInstanceSteps = (from ris in recipeInstanceStep.RecipeInstance.RecipeInstanceSteps where ris.Finished == null select ris).ToList();
            if (uncompletedRecipeInstanceSteps.Count == 0)
                recipeInstanceStep.RecipeInstance.IsCompleted = true;

            //can the meal be marked as completed?
            var uncompletedRecipeInstances = (from x in recipeInstanceStep.RecipeInstance.Meal.RecipeInstances where !x.IsCompleted select x).ToList();
            if (uncompletedRecipeInstances.Count == 0)
                recipeInstanceStep.RecipeInstance.Meal.IsCompleted = true;

            db.SaveChanges();

            return recipeInstanceStep;
        }

        public static List<RecipeInstanceStep> GetNextSteps(int recipeInstanceId)
        {
            var rv = new List<RecipeInstanceStep>();
            var recipeInstance = GetRecipeInstance(recipeInstanceId);
            foreach (var recipeInstanceStep in recipeInstance.RecipeInstanceSteps.Where(ris => ris.Finished == null).ToList())
            {
                //If first in line
                if (recipeInstanceStep.Step.ParentStepDependancies.Count == 0)
                {
                    rv.Add(recipeInstanceStep);
                    continue;
                }

                //If all immediately previous steps are finished
                var allFinished = true;
                foreach (var stepDependancy in recipeInstanceStep.Step.ParentStepDependancies)
                {
                    var instanceStep = recipeInstance.RecipeInstanceSteps.Single(ris=>ris.StepId==stepDependancy.ParentStepId);
                    if (instanceStep.Finished == null) allFinished = false;
                }
                if(allFinished)rv.Add(recipeInstanceStep);
            }

            return rv;
        }
    }
}
