using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class StepRecipeDependancyHandler
    {
        public static StepRecipeDependancy CreateStepRecipeDependancy(int stepId, int recipeId)
        {
            var db = new YesChefContext();
            var stepRecipeDependancy = db.StepRecipeDependancies.Add(new StepRecipeDependancy
            {
                StepId = stepId,
                RecipeId = recipeId
            });
            db.SaveChanges();

            stepRecipeDependancy = GetstepRecipeDependancy(stepRecipeDependancy.Id);

            return stepRecipeDependancy;
        }

        private static StepRecipeDependancy GetstepRecipeDependancy(int stepRecipeDependancyId)
        {
            var db = new YesChefContext();
            var stepRecipeDependancy = db.StepRecipeDependancies.Find(stepRecipeDependancyId);
            return stepRecipeDependancy;
        }
    }
}
