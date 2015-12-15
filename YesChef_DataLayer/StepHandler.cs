using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class StepHandler
    {
        public static Step CreateStep(string description, int minutesDuration, int recipeId, bool isFreeTime = false)
        {
            var db = new YesChefContext();
            var step = db.Steps.Add(new Step
            {
                Description = description,
                MinutesDuration = minutesDuration,
                RecipeId = recipeId,
                IsFreeTime = isFreeTime
            });
            db.SaveChanges();

            step = GetStep(step.Id);

            return step;
        }

        public static Step GetStep(int stepId)
        {
            var db = new YesChefContext();
            return db.Steps
                .Include(s => s.Recipe)
                .Single(s => s.Id == stepId);
        }

        public static List<Step> GetChildSteps(int recipeInstanceStepId)
        {
            var recipeInstanceStep = RecipeInstanceStepHandler.GetRecipeInstanceStep(recipeInstanceStepId);
            var db = new YesChefContext();
            var steps =
                (from sd in db.StepDependancies where sd.ParentStepId == recipeInstanceStep.StepId select sd.ChildStep)
                    .ToList();

            //Pertinant recipe dependancies?
            if (recipeInstanceStep.Step.ChildStepDependancies.Count == 0)
            {
                //Is a dependancy on main recipe?
                if (recipeInstanceStep.RecipeInstance.RecipeId != recipeInstanceStep.Step.RecipeId)
                {
                    var recipeDependancy = (from srd in db.StepRecipeDependancies
                                            where srd.RecipeId== recipeInstanceStep.Step.RecipeId 
                                            && srd.DependantStep.RecipeId== recipeInstanceStep.RecipeInstance.RecipeId
                                            select srd).SingleOrDefault();
                    if(recipeDependancy!=null)
                        steps.Add(recipeDependancy.DependantStep);
                }
            }

            return steps;
        }

        public static List<Step> GetParentSteps(int recipeInstanceStepId)
        {
            var recipeInstanceStep = RecipeInstanceStepHandler.GetRecipeInstanceStep(recipeInstanceStepId);
            var db = new YesChefContext();
            return (from sd in db.StepDependancies where sd.ChildStepId == recipeInstanceStep.Step.Id select sd.ParentStep).ToList();
        }
    }
}
