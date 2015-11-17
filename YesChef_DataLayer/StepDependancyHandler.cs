using System;
using System.Linq;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class StepDependancyHandler
    {
        public static StepDependancy CreateStepDependancy(Step parentStep, Step dependantStep)
        {
            var db = new YesChefContext();

            //Ensure dependancy doesn't already exist
            var existingStepDependancies = (from sd in db.StepDependancies
                                            where sd.ParentStepId==parentStep.Id 
                                            && sd.ChildStepId==dependantStep.Id
                                            select sd).ToList();
            if(existingStepDependancies.Count>0)
                throw new Exception("This Step Dependancy already exists");

            var stepDependancy = db.StepDependancies.Add(new StepDependancy
            {
                ParentStep = parentStep,
                ChildStep = dependantStep
            });
            db.SaveChanges();

            return stepDependancy;
        }
    }
}
