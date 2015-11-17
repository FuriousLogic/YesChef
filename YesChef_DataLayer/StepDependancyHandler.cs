using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class StepDependancyHandler
    {
        public static StepDependancy CreateStepDependancy(Step parentStep, Step dependantStep)
        {
            var db = new YesChefContext();
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
