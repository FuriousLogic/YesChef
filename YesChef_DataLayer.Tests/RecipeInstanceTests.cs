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
    public class RecipeInstanceTests
    {
        [Test]
        public void ShouldCreateRecipeInstance()
        {
            var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe: recipe, meal: meal);
            Assert.That(recipeInstance, Is.Not.Null);
            Assert.That(recipeInstance.Meal.Id, Is.EqualTo(meal.Id));
            Assert.That(recipeInstance.Id, Is.GreaterThan(0));
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
            var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef);
            var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe, meal);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(recipe, Is.Not.Null);
            Assert.That(step1, Is.Not.Null);
            Assert.That(step2, Is.Not.Null);
            Assert.That(step3, Is.Not.Null);
            Assert.That(recipeInstance, Is.Not.Null);

            //Check Times on recipe instance steps

            Assert.Fail();
        }
    }
}
