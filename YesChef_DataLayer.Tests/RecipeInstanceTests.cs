using System;
using NUnit.Framework;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class RecipeInstanceTests
    {
        [Test]
        public void ShouldCreateRecipeInstance()
        {
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
            StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipeId: recipe.Id, mealId: meal.Id);

            Assert.That(recipeInstance, Is.Not.Null);
            Assert.That(recipeInstance.Meal.Id, Is.EqualTo(meal.Id));
            Assert.That(recipeInstance.Id, Is.GreaterThan(0));
            Assert.That(recipeInstance.RecipeInstanceSteps.Count,Is.EqualTo(2));
        }

        [Test]
        public void ShouldPredictTimingsForSeries()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step2.Id, step3.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            var minutesToFinish = RecipeInstanceHandler.GetMinutesToFinish(recipeInstance.Id);
            Assert.That(minutesToFinish, Is.EqualTo(15));
        }
        [Test]
        public void ShouldPredictTimingsForParallelSeries()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");

            var recipe1 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe1.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe1.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe1.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step2.Id, step3.Id);

            var recipe2= RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step4 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe2.Id);
            var step5 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe2.Id);
            var step6 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe2.Id);
            StepDependancyHandler.CreateStepDependancy(step4.Id, step5.Id);
            StepDependancyHandler.CreateStepDependancy(step5.Id, step6.Id);

            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance1 = RecipeInstanceHandler.CreateRecipeInstance(recipe1.Id, meal.Id);
            var recipeInstance2 = RecipeInstanceHandler.CreateRecipeInstance(recipe2.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe1, Is.Not.Null);
            Assert.That(recipe2, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(step4, Is.Not.Null);
            Assert.That(step5, Is.Not.Null);
            Assert.That(step6, Is.Not.Null);
            Assert.That(recipeInstance1, Is.Not.Null);
            Assert.That(recipeInstance2, Is.Not.Null);

            var minutesToFinish = MealHandler.GetMinutesToFinish(meal.Id);
            Assert.That(minutesToFinish, Is.EqualTo(18));
        }
        [Test]
        public void ShouldPredictTimingsForSplit()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step3.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            var minutesToFinish = RecipeInstanceHandler.GetMinutesToFinish(recipeInstance.Id);
            Assert.That(minutesToFinish, Is.EqualTo(10));
        }
        [Test]
        public void ShouldPredictTimingsForConvergance()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step3.Id, step2.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            var minutesToFinish = RecipeInstanceHandler.GetMinutesToFinish(recipeInstance.Id);
            Assert.That(minutesToFinish, Is.EqualTo(11));
        }

        [Test]
        public void ShouldStartStep()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step2.Id, step3.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            var recipeInstanceStep = RecipeInstanceHandler.YesChef(recipeInstance.Id, step1.Id);

            Assert.That(recipeInstanceStep.StepId, Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceStep.Started,Is.Not.Null);
        }
        [Test]
        public void ShouldRecalculateRecipeTimeAfterStepCompletion()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step2.Id, step3.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            var recipeInstanceStep = RecipeInstanceHandler.YesChef(recipeInstance.Id, step1.Id);
            Assert.That(recipeInstanceStep.StepId,Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceStep.Started, Is.Not.Null);

            recipeInstanceStep = RecipeInstanceHandler.FinishedChef(recipeInstance.Id, step1.Id);
            Assert.That(recipeInstanceStep.StepId, Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceStep.Finished, Is.Not.Null);

            var minutesToFinish = RecipeInstanceHandler.GetMinutesToFinish(recipeInstance.Id);
            Assert.That(minutesToFinish, Is.EqualTo(11));
        }

        [Test]
        public void ShouldThrowErrorIfFinishStepBeforeStarting()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            Assert.That(()=> RecipeInstanceHandler.FinishedChef(recipeInstance.Id, step1.Id), Throws.Exception);
        }

        [Test]
        public void ShouldCompleteRecipe()
        {
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step2.Id, step3.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            //Process steps
            RecipeInstanceHandler.YesChef(recipeInstance.Id, step1.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance.Id, step1.Id);
            RecipeInstanceHandler.YesChef(recipeInstance.Id, step2.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance.Id, step2.Id);
            RecipeInstanceHandler.YesChef(recipeInstance.Id, step3.Id);

            //Check value is still false
            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            Assert.That(recipeInstance.IsCompleted,Is.False);

            //Finish recipe
            RecipeInstanceHandler.FinishedChef(recipeInstance.Id, step3.Id);

            //Ensure it's marked as finished
            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            Assert.That(recipeInstance.IsCompleted, Is.True);
        }
        [Test]
        public void ShouldPresentCorrectStepInSeriesRecipe() { Assert.Fail(); }
        [Test]
        public void ShouldPresentCorrectStepInMultiPathRecipe() { Assert.Fail(); }
        [Test]
        public void ShouldPresentCorrectStepInMultiRecipeMeal() { Assert.Fail(); }
    }
}
