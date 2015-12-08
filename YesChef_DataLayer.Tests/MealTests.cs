using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class MealTests
    {
        [Test]
        public void ShouldCreateMealAndNav()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal(name, sousChef.Id);
            Assert.That(meal, Is.Not.Null);
            Assert.That(meal.Name, Is.EqualTo(name));
            Assert.That(meal.Id, Is.GreaterThan(0));

            //Chef nav
            sousChef = SousChefHandler.GetSousChef(sousChef.Id);
            Assert.That(sousChef.Meals, Is.Not.Null);
            Assert.That(sousChef.Meals.Count, Is.EqualTo(1));
            Assert.That(sousChef.Meals.First().Id,Is.EqualTo(meal.Id));

            //Meal nav
            Assert.That(meal.SousChef, Is.Not.Null);
            Assert.That(meal.SousChef.Id, Is.EqualTo(sousChef.Id));
        }
        [Test]
        public void ShouldCreateMealWithRecipe()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var meal = MealHandler.CreateMeal(name, sousChef.Id, recipe.Id);
            Assert.That(meal, Is.Not.Null);
            Assert.That(meal.RecipeInstances.Count, Is.EqualTo(1));
        }
        [Test]
        public void ShouldCreateMealWithRecipies()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe1 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var recipe2 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var meal = MealHandler.CreateMeal(name, sousChef.Id, recipe1.Id);
            Assert.That(meal, Is.Not.Null);
            meal = MealHandler.AddRecipe(meal.Id, recipe2.Id);
            Assert.That(meal.RecipeInstances.Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldCompleteMultiRecipeMeal()
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

            //Complete Recipe1
            Assert.That(recipeInstance1.IsCompleted, Is.False);
            RecipeInstanceHandler.YesChef(recipeInstance1.Id, step1.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance1.Id, step1.Id);
            RecipeInstanceHandler.YesChef(recipeInstance1.Id, step2.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance1.Id, step2.Id);
            RecipeInstanceHandler.YesChef(recipeInstance1.Id, step3.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance1.Id, step3.Id);
            recipeInstance1 = RecipeInstanceHandler.GetRecipeInstance(recipeInstance1.Id);
            Assert.That(recipeInstance1.IsCompleted, Is.True);

            //Process Recipe2
            Assert.That(recipeInstance2.IsCompleted, Is.False);
            RecipeInstanceHandler.YesChef(recipeInstance2.Id, step4.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance2.Id, step4.Id);
            RecipeInstanceHandler.YesChef(recipeInstance2.Id, step5.Id);
            RecipeInstanceHandler.FinishedChef(recipeInstance2.Id, step5.Id);
            RecipeInstanceHandler.YesChef(recipeInstance2.Id, step6.Id);

            meal = MealHandler.GetMeal(meal.Id);
            Assert.That(meal.IsCompleted, Is.False);

            //Finish Recipe2
            RecipeInstanceHandler.FinishedChef(recipeInstance2.Id, step6.Id);
            recipeInstance2 = RecipeInstanceHandler.GetRecipeInstance(recipeInstance2.Id);
            Assert.That(recipeInstance2.IsCompleted, Is.True);

            //Ensure that meal is marked as completed
            meal = MealHandler.GetMeal(meal.Id);
            Assert.That(meal.IsCompleted, Is.True);
        }

        [Test]
        public void ShouldPresentCorrectStepsForMultiRecipeMeal()
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
            RecipeInstanceHandler.CreateRecipeInstance(recipe1.Id, meal.Id);
            RecipeInstanceHandler.CreateRecipeInstance(recipe2.Id, meal.Id);

            var recipeInstanceSteps = MealHandler.GetNextSteps(meal.Id);
            Assert.That(recipeInstanceSteps.Count,Is.EqualTo(2));
            Assert.That(recipeInstanceSteps, Has.Exactly(1).Property("StepId").EqualTo(step1.Id));
            Assert.That(recipeInstanceSteps, Has.Exactly(1).Property("StepId").EqualTo(step4.Id));
        }
    }
}
