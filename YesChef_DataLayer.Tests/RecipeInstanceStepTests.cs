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
        public void ShouldGetRecipeInstanceStep()
        {
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id, recipe.Id);

            var recipeInstance = meal.RecipeInstances.Single(ri=>ri.RecipeId==recipe.Id);
            var recipeInstanceStep = recipeInstance.RecipeInstanceSteps.FirstOrDefault();
            Assert.That(recipeInstanceStep,Is.Not.Null);

            var instanceStep = RecipeInstanceStepHandler.GetRecipeInstanceStep(recipeInstanceStep.Id);
            Assert.That(instanceStep, Is.Not.Null);
            Assert.That(recipeInstanceStep.Id,Is.EqualTo(instanceStep.Id));
            Assert.That(recipeInstanceStep.Step, Is.Not.Null);
            Assert.That(recipeInstanceStep.RecipeInstance, Is.Not.Null);
        }

        [Test]
        public void ShouldGetChildSteps()
        {
            var recipe = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var childStep1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var childStep2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            Assert.That(recipe, !Is.Null);
            Assert.That(step, !Is.Null);
            Assert.That(childStep1, !Is.Null);
            Assert.That(childStep2, !Is.Null);

            var stepDependancy1 = StepDependancyHandler.CreateStepDependancy(step.Id, childStep1.Id);
            var stepDependancy2 = StepDependancyHandler.CreateStepDependancy(step.Id, childStep2.Id);
            Assert.That(stepDependancy1, !Is.Null);
            Assert.That(stepDependancy2, !Is.Null);

            Assert.That(StepHandler.GetChildSteps(step.Id).Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetParentSteps()
        {
            var recipe = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var parentStep1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var parentStep2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            Assert.That(recipe, !Is.Null);
            Assert.That(step, !Is.Null);
            Assert.That(parentStep1, !Is.Null);
            Assert.That(parentStep2, !Is.Null);

            var stepDependancy1 = StepDependancyHandler.CreateStepDependancy(parentStep1.Id, step.Id);
            var stepDependancy2 = StepDependancyHandler.CreateStepDependancy(parentStep2.Id, step.Id);
            Assert.That(stepDependancy1, !Is.Null);
            Assert.That(stepDependancy2, !Is.Null);

            Assert.That(StepHandler.GetChildSteps(parentStep1.Id).Count, Is.EqualTo(1));
            Assert.That(StepHandler.GetChildSteps(parentStep2.Id).Count, Is.EqualTo(1));
            Assert.That(StepHandler.GetParentSteps(step.Id).Count, Is.EqualTo(2));
        }

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
            Assert.That(recipeInstance, Is.Not.Null);
            Assert.That(recipeInstance.Id, Is.GreaterThan(0));
            Assert.That(recipeInstance.RecipeInstanceSteps.Count, Is.EqualTo(3));
        }
    }
}
