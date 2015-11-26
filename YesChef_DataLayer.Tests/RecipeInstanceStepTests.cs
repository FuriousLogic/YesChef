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
    [Ignore]
    internal class RecipeInstanceStepTests
    {
        //[Test]
        //public void ShouldCreateRecipeInstanceStep()
        //{
        //    var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
        //    var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
        //    var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef, recipe);
        //    var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe, meal);
        //    var step = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
        //    var recipeInstanceStep = RecipeInstanceStepHandler.CreateRecipeInstanceStep(recipeInstance, step);
        //    Assert.That(recipeInstanceStep,Is.Not.Null);
        //    Assert.That(recipeInstanceStep.Id, Is.GreaterThan(0));
        //    Assert.That(recipeInstance.RecipeInstanceSteps.Count, Is.EqualTo(1));
        //}

        //[Test]
        //public void ShouldCreateAllRecipeInstanceStepsForRecipe()
        //{
        //    var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
        //    StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
        //    StepHandler.CreateStep($"desc {Guid.NewGuid()}", 2, recipe.Id);
        //    StepHandler.CreateStep($"desc {Guid.NewGuid()}", 3, recipe.Id);
        //    var sousChef = SousChefHandler.CreateSousChef($"name {Guid.NewGuid()}", "1@1.com", "password");
        //    var meal = MealHandler.CreateMeal($"name {Guid.NewGuid()}", sousChef, recipe);
        //    var recipeInstance = RecipeInstanceHandler.CreateRecipeInstance(recipe, meal);
        //    recipeInstance = RecipeInstanceStepHandler.CreateRecipeInstanceSteps(recipeInstance);
        //    Assert.That(recipeInstance, Is.Not.Null);
        //    Assert.That(recipeInstance.Id, Is.GreaterThan(0));
        //    Assert.That(recipeInstance.RecipeInstanceSteps.Count, Is.EqualTo(3));
        //}
    }
}
