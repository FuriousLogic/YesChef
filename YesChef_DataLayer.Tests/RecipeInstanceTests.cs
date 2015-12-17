using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            Assert.That(recipeInstance.RecipeInstanceSteps.Count, Is.EqualTo(2));
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

            var recipe2 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
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

            var recipeInstanceStep1 = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId == step1.Id);
            var recipeInstanceStep = RecipeInstanceHandler.YesChef(recipeInstanceStep1.Id);
            recipeInstanceStep = RecipeInstanceStepHandler.GetRecipeInstanceStep(recipeInstanceStep.Id);

            Assert.That(recipeInstanceStep.StepId, Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceStep.Started, Is.Not.Null);
        }
        [Test]
        public void ShouldCompleteStep()
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

            var recipeInstanceStep1 = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId == step1.Id);
            var recipeInstanceStep = RecipeInstanceHandler.YesChef(recipeInstanceStep1.Id);
            recipeInstanceStep = RecipeInstanceHandler.FinishedChef(recipeInstanceStep1.Id);
            recipeInstanceStep = RecipeInstanceStepHandler.GetRecipeInstanceStep(recipeInstanceStep.Id);

            Assert.That(recipeInstanceStep.StepId, Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceStep.Started, Is.Not.Null);
            Assert.That(recipeInstanceStep.Finished, Is.Not.Null);
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

            var recipeInstanceStep1 = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId == step1.Id);

            var recipeInstanceStep = RecipeInstanceHandler.YesChef(recipeInstanceStep1.Id);
            Assert.That(recipeInstanceStep.StepId, Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceStep.Started, Is.Not.Null);

            recipeInstanceStep = RecipeInstanceHandler.FinishedChef(recipeInstanceStep1.Id);
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

            var recipeInstanceStep = recipeInstance.RecipeInstanceSteps.Single(ris=>ris.StepId==step1.Id);

            Assert.That(() => RecipeInstanceHandler.FinishedChef(recipeInstanceStep.Id), Throws.Exception);
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

            var recipeInstanceStep1 = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId == step1.Id);
            var recipeInstanceStep2 = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId == step2.Id);
            var recipeInstanceStep3 = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId == step3.Id);

            //Process steps
            RecipeInstanceHandler.YesChef(recipeInstanceStep1.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstanceStep1.Id);
            RecipeInstanceHandler.YesChef(recipeInstanceStep2.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstanceStep2.Id);
            RecipeInstanceHandler.YesChef(recipeInstanceStep3.Id);

            //Check value is still false
            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            Assert.That(recipeInstance.IsCompleted, Is.False);

            //Finish recipe
            RecipeInstanceHandler.FinishedChef(recipeInstanceStep3.Id);

            //Ensure it's marked as finished
            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            Assert.That(recipeInstance.IsCompleted, Is.True);
        }

        [Test]
        public void ShouldPresentCorrectStepInSeriesRecipe()
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

            var recipeInstanceSteps = RecipeInstanceHandler.GetNextSteps(recipeInstance.Id);
            Assert.That(recipeInstanceSteps.Count, Is.EqualTo(1));
            Assert.That(recipeInstanceSteps[0].StepId, Is.EqualTo(step1.Id));
        }
        [Test]
        public void ShouldPresentCorrectStepInMultiPathRecipe()
        {   
            //Setup
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 4, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 5, recipe.Id);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 6, recipe.Id);
            var step4 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe.Id);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
            StepDependancyHandler.CreateStepDependancy(step3.Id, step4.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);

            var recipeInstanceSteps = RecipeInstanceHandler.GetNextSteps(recipeInstance.Id);
            Assert.That(recipeInstanceSteps.Count, Is.EqualTo(2));
            Assert.That(recipeInstanceSteps, Has.Exactly(1).Property("StepId").EqualTo(step1.Id));
            Assert.That(recipeInstanceSteps, Has.Exactly(1).Property("StepId").EqualTo(step3.Id));
        }

        [Test]
        public void ShouldPresentCorrectStepInSeriesRecipeAfterYesChef()
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

            var recipeInstanceStep = recipeInstance.RecipeInstanceSteps.Single(ris=>ris.StepId==step1.Id);
            RecipeInstanceHandler.YesChef(recipeInstanceStep.Id);

            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            var recipeInstanceSteps = RecipeInstanceHandler.GetNextSteps(recipeInstance.Id);
            Assert.That(recipeInstanceSteps.Count, Is.EqualTo(1));
            Assert.That(recipeInstanceSteps[0].StepId, Is.EqualTo(step1.Id));
            Assert.That(recipeInstanceSteps[0].Started, Is.Not.Null);
            Assert.That(recipeInstanceSteps[0].Finished, Is.Null);
        }

        [Test]
        public void ShouldPresentCorrectStepInSeriesRecipeAfterFinishedChef()
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

            var recipeInstanceStep = recipeInstance.RecipeInstanceSteps.Single(ris=>ris.StepId==step1.Id);
            RecipeInstanceHandler.YesChef(recipeInstanceStep.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstanceStep.Id);

            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            var recipeInstanceSteps = RecipeInstanceHandler.GetNextSteps(recipeInstance.Id);
            Assert.That(recipeInstanceSteps.Count, Is.EqualTo(1));
            Assert.That(recipeInstanceSteps[0].StepId, Is.EqualTo(step2.Id));
            Assert.That(recipeInstanceSteps[0].Started, Is.Null);
            Assert.That(recipeInstanceSteps[0].Finished, Is.Null);
        }

        [Test]
        public void ShouldGiveCorrectTimeForHybridRecipe()
        {
            //Create Recipe1
            var parentRecipe = RecipeHandler.CreateRecipe($"Parent {Guid.NewGuid()}");
            var stepP1 = StepHandler.CreateStep($"P1 {Guid.NewGuid()}", 5, parentRecipe.Id);
            var stepP2 = StepHandler.CreateStep($"P2 {Guid.NewGuid()}", 3, parentRecipe.Id);
            StepDependancyHandler.CreateStepDependancy(stepP1.Id, stepP2.Id);

            //Create Recipe2
            var childRecipe = RecipeHandler.CreateRecipe($"Child {Guid.NewGuid()}");
            var stepC1 = StepHandler.CreateStep($"C1 {Guid.NewGuid()}", 3, childRecipe.Id);
            var stepC2 = StepHandler.CreateStep($"C2 {Guid.NewGuid()}", 4, childRecipe.Id);
            StepDependancyHandler.CreateStepDependancy(stepC1.Id, stepC2.Id);

            //Make a step4 dependant on recipe1
            StepRecipeDependancyHandler.CreateStepRecipeDependancy(stepC2.Id, parentRecipe.Id);

            //Create Meal Based on Recipe2
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal($"Name {Guid.NewGuid()}", sousChef.Id, childRecipe.Id);

            //Get time for Recipe2 - should include recipe 1
            var minutesToFinish = MealHandler.GetMinutesToFinish(meal.Id);

            Assert.That(minutesToFinish,Is.EqualTo(12));
        }

        [Test]
        public void ShouldGiveCorrectTimeForStartedHybrid()
        {
            //Create Recipe1
            var parentRecipe = RecipeHandler.CreateRecipe($"Parent {Guid.NewGuid()}");
            var stepP1 = StepHandler.CreateStep($"P1 {Guid.NewGuid()}", 5, parentRecipe.Id);
            var stepP2 = StepHandler.CreateStep($"P2 {Guid.NewGuid()}", 3, parentRecipe.Id);
            StepDependancyHandler.CreateStepDependancy(stepP1.Id, stepP2.Id);

            //Create Recipe2
            var childRecipe = RecipeHandler.CreateRecipe($"Child {Guid.NewGuid()}");
            var stepC1 = StepHandler.CreateStep($"C1 {Guid.NewGuid()}", 4, childRecipe.Id);
            var stepC2 = StepHandler.CreateStep($"C2 {Guid.NewGuid()}", 4, childRecipe.Id);
            StepDependancyHandler.CreateStepDependancy(stepC1.Id, stepC2.Id);

            //Make a step4 dependant on recipe1
            StepRecipeDependancyHandler.CreateStepRecipeDependancy(stepC2.Id, parentRecipe.Id);

            //Create Meal Based on Recipe2
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal($"Name {Guid.NewGuid()}", sousChef.Id, childRecipe.Id);


            //Get time for Recipe2 - should include recipe 1
            var minutesToFinish = MealHandler.GetMinutesToFinish(meal.Id);
            Assert.That(minutesToFinish, Is.EqualTo(12));

            //Complete a step
            var recipeInstance = meal.RecipeInstances.FirstOrDefault();
            Assert.That(recipeInstance, Is.Not.Null);
            var recipeInstanceStep = recipeInstance.RecipeInstanceSteps.Single(ris => ris.StepId==stepP1.Id);
            RecipeInstanceHandler.YesChef(recipeInstanceStep.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstanceStep.Id);

            //Get time for Recipe2 - should ignore recipe 1
            minutesToFinish = MealHandler.GetMinutesToFinish(meal.Id);
            Assert.That(minutesToFinish, Is.EqualTo(8));
        }

        [Test]
        public void ShouldPresentCorrectStepsForHybridRecipe()
        {
            //Create Recipe1
            var parentRecipe = RecipeHandler.CreateRecipe($"Parent {Guid.NewGuid()}");
            var stepP1 = StepHandler.CreateStep($"P1 {Guid.NewGuid()}", 5, parentRecipe.Id);
            var stepP2 = StepHandler.CreateStep($"P2 {Guid.NewGuid()}", 3, parentRecipe.Id);
            StepDependancyHandler.CreateStepDependancy(stepP1.Id, stepP2.Id);

            //Create Recipe2
            var childRecipe = RecipeHandler.CreateRecipe($"Child {Guid.NewGuid()}");
            var stepC1 = StepHandler.CreateStep($"C1 {Guid.NewGuid()}", 3, childRecipe.Id);
            var stepC2 = StepHandler.CreateStep($"C2 {Guid.NewGuid()}", 4, childRecipe.Id);
            StepDependancyHandler.CreateStepDependancy(stepC1.Id, stepC2.Id);

            //Make a step4 dependant on recipe1
            StepRecipeDependancyHandler.CreateStepRecipeDependancy(stepC2.Id, parentRecipe.Id);

            //Create Meal Based on Recipe2
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal($"Name {Guid.NewGuid()}", sousChef.Id, childRecipe.Id);

            var recipeInstanceSteps = MealHandler.GetNextSteps(meal.Id);
            Assert.That(recipeInstanceSteps.Count,Is.EqualTo(2));
            Assert.That(recipeInstanceSteps, Has.Exactly(1).Property("StepId").EqualTo(stepC1.Id));
            Assert.That(recipeInstanceSteps, Has.Exactly(1).Property("StepId").EqualTo(stepP1.Id));
        }
    }
}
