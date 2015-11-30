using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    internal class RecipeInstanceStepTests
    {
        [Test]
        public void ShouldCreateRecipeInstanceStep()
        {
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id, recipe.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            var step = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
            var recipeInstanceStep = RecipeInstanceStepHandler.CreateRecipeInstanceStep(recipeInstance.Id, step.Id);
            Assert.That(recipeInstanceStep, Is.Not.Null);
            Assert.That(recipeInstanceStep.Id, Is.GreaterThan(0));

            recipeInstance = RecipeInstanceHandler.GetRecipeInstance(recipeInstance.Id);
            Assert.That(recipeInstance.RecipeInstanceSteps.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldCreateAllRecipeInstanceStepsForRecipe()
        {
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
            StepHandler.CreateStep($"desc {Guid.NewGuid()}", 2, recipe.Id);
            StepHandler.CreateStep($"desc {Guid.NewGuid()}", 3, recipe.Id);
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id, recipe.Id);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe.Id, meal.Id);
            recipeInstance = RecipeInstanceStepHandler.CreateRecipeInstanceSteps(recipeInstance.Id);
            Assert.That(recipeInstance, Is.Not.Null);
            Assert.That(recipeInstance.Id, Is.GreaterThan(0));
            Assert.That(recipeInstance.RecipeInstanceSteps.Count, Is.EqualTo(3));
        }
    }
}
