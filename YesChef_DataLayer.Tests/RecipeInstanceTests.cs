using System;
using NUnit.Framework;

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
            Assert.That(minutesToFinish,Is.EqualTo(15));
        }
    }
}
