using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            var recipeInstance = meal.RecipeInstances.Single(ri => ri.RecipeId == recipe.Id);
            var recipeInstanceStep = recipeInstance.RecipeInstanceSteps.FirstOrDefault();
            Assert.That(recipeInstanceStep, Is.Not.Null);

            var instanceStep = RecipeInstanceStepHandler.GetRecipeInstanceStep(recipeInstanceStep.Id);
            Assert.That(instanceStep, Is.Not.Null);
            Assert.That(recipeInstanceStep.Id, Is.EqualTo(instanceStep.Id));
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

            StepDependancyHandler.CreateStepDependancy(step.Id, childStep1.Id);
            StepDependancyHandler.CreateStepDependancy(step.Id, childStep2.Id);

            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.1", "password");
            var meal = MealHandler.CreateMeal($"Name {Guid.NewGuid()}", sousChef.Id, recipe.Id);

            var recipeInstance = meal.RecipeInstances.FirstOrDefault();
            Assert.IsNotNull(recipeInstance);
            Assert.IsNotNull(recipeInstance.RecipeInstanceSteps);
            Assert.IsTrue(recipeInstance.RecipeInstanceSteps.Count > 0);

            var recipeInstanceStep = (from x in recipeInstance.RecipeInstanceSteps where x.StepId == step.Id select x).FirstOrDefault();
            Assert.IsNotNull(recipeInstanceStep);
            Assert.That(StepHandler.GetChildSteps(recipeInstanceStep.Id).Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetChildStepsForHybrid()
        {
            //Host recipe
            var r1 = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var r1S1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r1.Id);
            var r1S2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r1.Id);
            StepDependancyHandler.CreateStepDependancy(r1S1.Id, r1S2.Id);

            //Parasite recipe
            var r2 = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var r2S1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r2.Id);
            var r2S2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r2.Id);
            StepDependancyHandler.CreateStepDependancy(r2S1.Id, r2S2.Id);

            //Attach
            StepRecipeDependancyHandler.CreateStepRecipeDependancy(r1S2.Id, r2.Id);

            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.1", "password");
            var meal = MealHandler.CreateMeal($"Name {Guid.NewGuid()}", sousChef.Id, r1.Id);

            var recipeInstance = meal.RecipeInstances.FirstOrDefault();
            Assert.IsNotNull(recipeInstance);
            Assert.IsNotNull(recipeInstance.RecipeInstanceSteps);
            Assert.IsTrue(recipeInstance.RecipeInstanceSteps.Count > 0);

            var recipeInstanceStep = (from x in recipeInstance.RecipeInstanceSteps where x.StepId == r2S2.Id select x).FirstOrDefault();
            Assert.IsNotNull(recipeInstanceStep);

            var childSteps = StepHandler.GetChildSteps(recipeInstanceStep.Id);
            Assert.That(childSteps.Count, Is.EqualTo(1));
            Assert.IsTrue(childSteps[0].Id==r1S2.Id);
        }

        [Test]
        public void ShouldGetParentSteps()
        {
            var recipe = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var parentStep1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var parentStep2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);

            StepDependancyHandler.CreateStepDependancy(parentStep1.Id, step.Id);
            StepDependancyHandler.CreateStepDependancy(parentStep2.Id, step.Id);

            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}","1@1.1","password");
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}",sousChef.Id,recipe.Id);

            var recipeInstance = meal.RecipeInstances.First();
            var recipeInstanceStep = (from x in recipeInstance.RecipeInstanceSteps where x.StepId==step.Id select x).FirstOrDefault();
            Assert.IsNotNull(recipeInstanceStep);

            var parentSteps = StepHandler.GetParentSteps(recipeInstanceStep.Id);
            Assert.That(parentSteps.Count, Is.EqualTo(2));
            Assert.That(parentSteps, Has.Exactly(1).Property("Id").EqualTo(parentStep1.Id));
            Assert.That(parentSteps, Has.Exactly(1).Property("Id").EqualTo(parentStep2.Id));
        }

        [Test]
        public void ShouldGetParentStepsForHybrid()
        {
            //Host Recipe
            var r1 = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var r1S1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r1.Id);
            var r1S2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r1.Id);
            StepDependancyHandler.CreateStepDependancy(r1S1.Id, r1S2.Id);

            //Parasite Recipe
            var r2 = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var r2S1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r2.Id);
            var r2S2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, r2.Id);
            StepDependancyHandler.CreateStepDependancy(r2S1.Id, r2S2.Id);

            //Attach
            StepRecipeDependancyHandler.CreateStepRecipeDependancy(r1S2.Id, r2.Id);

            //Meal
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.1", "password");
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef.Id, r1.Id);

            var recipeInstance = meal.RecipeInstances.First();
            var recipeInstanceStep = (from x in recipeInstance.RecipeInstanceSteps where x.StepId == r1S2.Id select x).FirstOrDefault();
            Assert.IsNotNull(recipeInstanceStep);

            var parentSteps = StepHandler.GetParentSteps(recipeInstanceStep.Id);
            Assert.That(parentSteps.Count, Is.EqualTo(2));
            Assert.That(parentSteps, Has.Exactly(1).Property("Id").EqualTo(r1S1.Id));
            Assert.That(parentSteps, Has.Exactly(1).Property("Id").EqualTo(r2S2.Id));
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
