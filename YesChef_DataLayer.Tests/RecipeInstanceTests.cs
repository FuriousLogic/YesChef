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
            Assert.That(recipeInstance,Is.Not.Null);
            Assert.That(recipeInstance.Meal.Id,Is.EqualTo(meal.Id));
            Assert.That(recipeInstance.Id, Is.GreaterThan(0));
        }
    }
}
