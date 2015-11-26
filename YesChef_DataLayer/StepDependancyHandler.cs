using System;
using System.Linq;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class StepDependancyHandler
    {
        public static StepDependancy CreateStepDependancy(int parentStepId, int childStepId)
        {
            var db = new YesChefContext();

            //Ensure dependancy doesn't already exist
            var existingStepDependancies = (from sd in db.StepDependancies
                                            where sd.ParentStepId == parentStepId
                                            && sd.ChildStepId == childStepId
                                            select sd).ToList();
            if (existingStepDependancies.Count > 0)
                throw new Exception("This Step Dependancy already exists");

            var stepDependancy = db.StepDependancies.Add(new StepDependancy
            {
                ParentStepId = parentStepId,
                ChildStepId = childStepId
            });
            db.SaveChanges();

            return stepDependancy;
        }
    }
}
